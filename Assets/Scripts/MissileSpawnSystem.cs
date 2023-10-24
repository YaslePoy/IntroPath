using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MissileSpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MissleConfig>();
    }

    // Update is called once per frame
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var cfg = SystemAPI.GetSingleton<MissleConfig>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var missle = state.EntityManager.Instantiate(cfg.prefab);
            var spawnPos = SystemAPI.GetComponent<LocalToWorld>(cfg.spawn).Position;
            state.EntityManager.SetComponentData(missle, new LocalTransform
            {
                Position = spawnPos,
                Rotation = quaternion.identity,
                Scale = 1
            });
            var current = state.EntityManager.GetComponentData<Missile>(missle);
            current.direction = Vector3.forward;
            state.EntityManager.SetComponentData(missle, current);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            var player = SystemAPI.QueryBuilder().WithAll<Player>().Build().ToEntityArray(Allocator.Temp)[0];
            state.EntityManager.DestroyEntity(player);
        }
    }
}
