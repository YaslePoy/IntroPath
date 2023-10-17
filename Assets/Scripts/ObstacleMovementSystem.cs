using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct ObstacleMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var t = SystemAPI.Time.DeltaTime;
        //foreach (var (obst,settings) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Obstacle>>())
        //{
        //    var start = obst.ValueRO.Position;
        //    start.z -= settings.ValueRO.speed * t;
        //    obst.ValueRW.Position = start;
        //}
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var job = new ObstacleJob
        {
            ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged),
            DeltaTime = t
        };
        job.Schedule();
    }
}

[BurstCompile]
public partial struct ObstacleJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float DeltaTime;

    void Execute(Entity entity, ref LocalTransform transform, ref Obstacle obstacle)
    {
        if (transform.Position.z > -1)
            transform.Position = transform.Position - new float3(0, 0, obstacle.Speed * DeltaTime);
        else
            ECB.DestroyEntity(entity);
    }
}