using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MissleSpawnAuthoring : MonoBehaviour
{
    public GameObject Missle;
    public Transform SpawnPoint;
    class Baker : Baker<MissleSpawnAuthoring>
    {
        public override void Bake(MissleSpawnAuthoring authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MissleConfig
            {
                prefab = GetEntity(authoring.Missle, TransformUsageFlags.Dynamic),
                spawn = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic),
            }); ;
        }
    }
}

public struct MissleConfig : IComponentData
{
    public Entity prefab, spawn;
}
