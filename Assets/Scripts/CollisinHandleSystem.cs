using System.Collections;
using System.Collections.Generic;
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

        state.Dependency = new CollisionJob
        {
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
}
[BurstCompile]
public struct CollisionJob : ICollisionEventsJob
{
    public void Execute(CollisionEvent collisionEvent)
    {
        Debug.Log("realy !!!!");
    }
}
