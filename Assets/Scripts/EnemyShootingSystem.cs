using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemyShootingSystem : ISystem
{
    // Start is called before the first frame update
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Player>();

    }

    // Update is called once per frame
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (UnityEngine.Random.value > 0.99)
        {
            var players = SystemAPI.QueryBuilder().WithAll<Player>().Build().ToEntityArray(Allocator.Temp);
            var player = players[0];
            var enemys = SystemAPI.QueryBuilder().WithAll<Enemy>().Build().ToEntityArray(Allocator.Temp);
            var i = (int)Math.Floor(UnityEngine.Random.value * (enemys.Length - 1));
            var selected = enemys[i];
            Debug.Log($"{DateTime.Now} Enemy {i} shooted");
            var data = SystemAPI.GetComponent<Enemy>(selected);
            var fromPoint = SystemAPI.GetComponent<LocalToWorld>(data.Spawn).Position;
            var toPoint = SystemAPI.GetComponent<LocalToWorld>(player).Position;
            var delta = fromPoint - toPoint;
            var len = math.sqrt(delta.x * delta.x + delta.y * delta.y + delta.z * delta.z);
            delta /= len;

            var cfg = SystemAPI.GetSingleton<MissleConfig>();
            var missle = state.EntityManager.Instantiate(cfg.prefab);
            state.EntityManager.SetComponentData(missle, new LocalTransform
            {
                Position = fromPoint,
                Rotation = quaternion.LookRotation(delta, new float3(0, 1, 0)),
                Scale = 1
            });
            var current = state.EntityManager.GetComponentData<Missile>(missle);
            current.direction = -delta;
            state.EntityManager.SetComponentData(missle, current);
        }
    }
}
