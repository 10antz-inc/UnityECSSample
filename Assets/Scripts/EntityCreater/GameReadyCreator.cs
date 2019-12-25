using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class GameReadyCreator : IEntityCreator
{
    public void Create(){
        var manager = World.Active.EntityManager;
        var archetype = manager.CreateArchetype(
            typeof(ECS.Components.GameReady),
            typeof(ECS.Components.InputInfomation),
            typeof(ECS.Components.ChangeGameStateRequester),
            typeof(ECS.Components.GameReadyEntity)  );
        manager.CreateEntity(archetype);



        var entityArray = manager.GetAllEntities();
        foreach(var entity in entityArray){

            if( manager.HasComponent<ECS.Components.Player>(entity) ) {
                manager.SetComponentData(entity, new Translation{ Value = new float3(0.0f, -4.5f, 0.0f   ) });
                break;
            }
        }
    }
}