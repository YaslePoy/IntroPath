using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ForcingAuthoring : MonoBehaviour
{
    class Baker : Baker<ForcingAuthoring>
    {
        public override void Bake(ForcingAuthoring authoring)
        {
            // GetEntity returns the baked Entity form of a GameObject.
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ForcingBody());
        }
    }
}
public struct ForcingBody : IComponentData
{

}
