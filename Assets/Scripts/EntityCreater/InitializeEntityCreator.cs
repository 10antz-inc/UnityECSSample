using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class InitializeEntityCreator : IInitialEntityCreator
{
    public void Create(){
        var manager = World.Active.EntityManager;
        var archetype = manager.CreateArchetype( typeof(ECS.Components.GameStateData));
        var entity = manager.CreateEntity(archetype);
        manager.SetComponentData(entity, new ECS.Components.GameStateData(){current = ECS.Components.GameState.None,prev = ECS.Components.GameState.None,next = ECS.Components.GameState.None } );

        archetype = manager.CreateArchetype( typeof(ECS.Components.ChangeGameStateRequester), typeof(ECS.Components.DestoryData));
        entity = manager.CreateEntity(archetype);
        manager.SetComponentData(entity, new ECS.Components.ChangeGameStateRequester(){ isRequest = 1, state = ECS.Components.GameState.Ready } );
    }
}