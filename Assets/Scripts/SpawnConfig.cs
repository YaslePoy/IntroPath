using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnConfig : MonoBehaviour
{
    public float Spawn;
    public GameObject obstacleModel;
    class Baker : Baker<SpawnConfig>
    {
        public override void Bake(SpawnConfig authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Config
            {
                spawnRate = authoring.Spawn,
                ObstaclePrefab = GetEntity(authoring.obstacleModel, TransformUsageFlags.Dynamic)
            });
        }
    }
}
public struct Config : IComponentData
{
    public float spawnRate;
    public Entity ObstaclePrefab;
}
