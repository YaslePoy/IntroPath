using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MissileAuthoring : MonoBehaviour
{
    public float speed;
    public float LiveTime;
    class Baker : Baker<MissileAuthoring>
    {
        public override void Bake(MissileAuthoring authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Missile
            {
                speed = authoring.speed,
                AliveTime = authoring.LiveTime,
            });
        }
    }
}

public struct Missile : IComponentData
{
    public float AliveTime;
    public float speed;
    public Vector3 direction;
}
