using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct ObstacleMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var horizontal = Input.GetAxis("Horizontal");
        var moveVec = new float3(horizontal, 0, 0) * Time.deltaTime;
        var t = Time.deltaTime;
        foreach (var (obst,settings) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Obstacle>>())
        {
            var start = obst.ValueRO.Position;
            start.z -= settings.ValueRO.speed * t;
            obst.ValueRW.Position = start;
        }
    }
}

public partial struct ObstacleJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float DeltaTime;
    void Execute(Entity entity, ref LocalTransform transform, ref Obstacle obstacle)
    {
        if(transform.Position.z == -1)
            ECB.DestroyEntity(entity);
        transform.Position = transform.Position - new float3(0, 0, obstacle.speed * DeltaTime);
    }
}
