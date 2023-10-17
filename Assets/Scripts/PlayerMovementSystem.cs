using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var horizontal = Input.GetAxis("Horizontal");
        var moveVec = new float3(horizontal, 0, 0) * Time.deltaTime;

        foreach ((RefRW<LocalTransform> player, RefRO<Player> settings) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Player>>())
        {
            var move = moveVec * settings.ValueRO.MoveSpeed;
            var newPos = player.ValueRO.Position + move;
            player.ValueRW.Position = newPos;
        }
    }
}