using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public Transform SpawnPoint;
    class Baker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Enemy() { Spawn = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic)});
        }
    }
}
public struct Enemy : IComponentData
{
    public Entity Spawn;
}
