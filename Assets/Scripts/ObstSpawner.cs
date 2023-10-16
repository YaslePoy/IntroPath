using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

public partial struct ObstSpawner : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
    }
    // Update is called once per frame
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var cfg = SystemAPI.GetSingleton<Config>();
        if (UnityEngine.Random.value < cfg.spawnRate)
        {
            return;
        }
        var spawnVec = new float3((UnityEngine.Random.value - 0.5f)*2, UnityEngine.Random.value,1) * 7;
        var spawnedObst = state.EntityManager.Instantiate(cfg.ObstaclePrefab);
        state.EntityManager.SetComponentData(spawnedObst, new LocalTransform
        {
            Position = spawnVec,
            Scale = 1,
            Rotation = quaternion.identity
        });
    }
}
