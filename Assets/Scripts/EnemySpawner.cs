using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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
        //if (UnityEngine.Random.value < cfg.spawnRate)
        //{
        //    return;
        //}
        var enemyCount = SystemAPI.QueryBuilder().WithAll<Enemy>().Build().ToEntityArray(Allocator.Temp).Length;
        while (enemyCount < cfg.TotalCount)
        {
            var position = new float3((UnityEngine.Random.value - 0.5f) * 2,0, 1 + UnityEngine.Random.value * 2) * 7;
            var spawnedObst = state.EntityManager.Instantiate(cfg.EnemyPrefab);
            state.EntityManager.SetComponentData(spawnedObst, new LocalTransform
            {
                Position = position,
                Scale = 1,
                Rotation = quaternion.identity
            });
            enemyCount++;
        }

    }
}