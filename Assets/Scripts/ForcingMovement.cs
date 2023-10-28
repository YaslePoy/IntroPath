using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public partial struct ForcingMovement : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ForcingBody>();
    }

    // Update is called once per frame
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        foreach (var body in SystemAPI.Query<RefRW<PhysicsVelocity>>().WithAll<ForcingBody>())
        {
            body.ValueRW.Linear = RandomF3() * 100;
            body.ValueRW.Angular = RandomF3() * 20;
            Debug.Log("Added");
        }
    }
    public float3 RandomF3() => new float3(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f);
}
