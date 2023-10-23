using Unity.Entities;
using UnityEngine;

public struct Config : IComponentData
{
    public Entity EnemyPrefab;
    public int TotalCount;
}

public class SpawnConfig : MonoBehaviour
{
    public GameObject enemyModel;
    public int count;
    class Baker : Baker<SpawnConfig>
    {
        public override void Bake(SpawnConfig authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Config
            {
                EnemyPrefab = GetEntity(authoring.enemyModel, TransformUsageFlags.Dynamic),
                TotalCount = authoring.count
            }) ;
        }
    }
}
