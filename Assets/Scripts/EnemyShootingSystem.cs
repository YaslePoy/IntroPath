using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
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
        if (UnityEngine.Random.value > 0.95)
        {
            var player = SystemAPI.QueryBuilder().WithAll<Player>().Build().ToEntityArray(Allocator.Temp)[0];
            Debug.Log($"{DateTime.Now} Enemy shooted");
        }

    }
}
