using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAuthoring : MonoBehaviour
{
        public float moveSpeed;

    class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Player
            {
                moveSpeed = authoring.moveSpeed,
            });
        }
    }
}
public struct Player : IComponentData, IEnableableComponent
{
    public float moveSpeed;
}
