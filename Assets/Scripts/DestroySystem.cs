using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public partial struct DestroySystem : ISystem
{
    // Start is called before the first frame update
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<DeleteMark>();
    }

    // Update is called once per frame
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //var destroing = SystemAPI.QueryBuilder().WithAll<DeleteMark>().Build().ToEntityArray(Allocator.Temp);
        //if(destroing.Length > 0)
        //{
        //    Debug.Log($"Destroing {destroing.Length} entitys");
        //}
        //foreach (var entity in destroing)
        //{
        //    state.EntityManager.DestroyEntity(entity);
        //}
        //destroing.Dispose();
    }
}
public struct DeleteMark : IComponentData
{
}
