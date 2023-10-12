using System.Collections;
using System.Collections.Generic;
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
                speed = authoring.fligtSpeed,
                damage = authoring.damage
            });
        }
    }
}
public struct Obstacle : IComponentData
{
    public float speed;
    public float damage;
}
