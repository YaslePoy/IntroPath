using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;


public partial struct CollisionHandleSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //Debug.Log("On Update!!!!");

        var job = new CollisionJob() { ECB = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged) };

        state.Dependency = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
}

[BurstCompile]
public struct CollisionJob : ICollisionEventsJob
{
    public EntityCommandBuffer ECB;
    public void Execute(CollisionEvent collisionEvent)
    {
        Debug.Log($"[{DateTime.Now}]CollisionEvent !!!! {collisionEvent.EntityA.Index} and {collisionEvent.EntityB.Index}");
        ECB.DestroyEntity(collisionEvent.EntityA);
        ECB.DestroyEntity(collisionEvent.EntityB);

    }
}
