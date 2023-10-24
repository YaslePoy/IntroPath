using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MissleMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<Missile>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var t = SystemAPI.Time.DeltaTime;
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var job = new MissleJob
        {
            ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged),
            DeltaTime = t
        };
        job.Schedule();
    }
}
[BurstCompile]
public partial struct MissleJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float DeltaTime;

    void Execute(Entity entity, ref LocalTransform transform, ref Missile missle)
    {
        missle.AliveTime -= DeltaTime;
        if (missle.AliveTime > 0)
            transform.Position +=  new float3(missle.direction * DeltaTime * missle.speed);

        else
            ECB.DestroyEntity(entity);
    }
}
