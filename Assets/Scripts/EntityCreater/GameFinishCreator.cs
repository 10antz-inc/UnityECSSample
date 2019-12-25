using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class GameFinishCreator : IEntityCreator
{
    const float BulletInterval = 1.5f;
    public void Create(){
        var manager = World.Active.EntityManager;

        //game
        var archetype = manager.CreateArchetype(
            typeof(ECS.Components.GameFinish),
            typeof(ECS.Components.InputInfomation),
            typeof(ECS.Components.ChangeGameStateRequester),
            typeof(ECS.Components.GameFinishEntity)  );
        manager.CreateEntity(archetype);
    }
}