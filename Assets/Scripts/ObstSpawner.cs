using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct ObstSpawner : ISystem
{

    // Update is called once per frame
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var cfg = SystemAPI.GetSingleton<Config>();
        if (UnityEngine.Random.value < cfg.spawnRate)
        {
            Debug.Log("NOT Spawned");
            return;
        }
        Debug.Log("Spawned");
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
