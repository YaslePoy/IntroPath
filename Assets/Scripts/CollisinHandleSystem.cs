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

        var job = new CollisionJob();

        state.Dependency = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
}

[BurstCompile]
public struct CollisionJob : ICollisionEventsJob
{
    public void Execute(CollisionEvent collisionEvent)
    {
        Debug.Log("CollisionEvent !!!!");
    }
}
