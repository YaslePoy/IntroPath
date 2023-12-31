using Unity.Entities;
using UnityEngine;

public struct Config : IComponentData
{
    public float spawnRate;
    public Entity ObstaclePrefab;
}

public class SpawnConfig : MonoBehaviour
{
    public float SpawnRate;
    public GameObject obstacleModel;

    class Baker : Baker<SpawnConfig>
    {
        public override void Bake(SpawnConfig authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Config
            {
                spawnRate = authoring.SpawnRate,
                ObstaclePrefab = GetEntity(authoring.obstacleModel, TransformUsageFlags.Dynamic)
            });
        }
    }
}
