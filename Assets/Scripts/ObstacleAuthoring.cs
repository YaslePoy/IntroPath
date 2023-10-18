using Unity.Entities;
using UnityEngine;

public class ObstacleAuthoring : MonoBehaviour
{
    public float fligtSpeed;
    public float damage;

    class Baker : Baker<ObstacleAuthoring>
    {
        public override void Bake(ObstacleAuthoring authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Obstacle
            {
                Speed = authoring.fligtSpeed,
                Damage = authoring.damage
            });
        }
    }
}
public struct Obstacle : IComponentData
{
    public float Speed;
    public float Damage;
}
