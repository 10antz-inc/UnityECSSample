using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class GameCreator : IEntityCreator
{
    const float BulletInterval = 1.5f;
    public void Create(){
        var manager = World.Active.EntityManager;

        //game
        var gameArchetype = manager.CreateArchetype( typeof(ECS.Components.Game), typeof(ECS.Components.InputInfomation), typeof(ECS.Components.GameEntity) );
        manager.CreateEntity(gameArchetype);

        //bulletGenerater
        var bulletGeneratorArchetype = manager.CreateArchetype( typeof(ECS.Components.GameEntity) ,typeof(ECS.Components.BulletGenerator), typeof(ECS.Components.Timer) );
        var bulletGenerator = manager.CreateEntity(bulletGeneratorArchetype);
        manager.SetComponentData(bulletGenerator, new ECS.Components.BulletGenerator(){ interval = BulletInterval } );

        //score
        var scoreArchetype = manager.CreateArchetype( typeof(ECS.Components.Score), typeof(ECS.Components.GameEntity));
        manager.CreateEntity(scoreArchetype);

    }
}