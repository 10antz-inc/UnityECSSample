using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class WallCreator : MonoBehaviour, IInitialMonoBehaviourEntityCreator
{
    [SerializeField]
    Mesh _mesh = null;
    [SerializeField]
    Material _material = null;

    public void Create(){
        var manager = World.Active.EntityManager;
        var archetype = manager.CreateArchetype(typeof(LocalToWorld),typeof(NonUniformScale),typeof(Translation), typeof(RenderMesh));
        var wallSize = 15.0f;
        var wallPos = 2.5f;
        for( int i=0; i<2; ++i ) {
            var entity = manager.CreateEntity(archetype);
            manager.SetComponentData(entity, new NonUniformScale { Value = new float3(1, wallSize, 1) } );
            manager.SetComponentData(entity, new Translation { Value = new float3( i==0 ? wallPos : -wallPos, 0, 0) } );

            manager.SetSharedComponentData(entity, new RenderMesh { mesh = _mesh, material = _material} );
        }
    }
}