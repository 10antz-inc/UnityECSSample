using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ECS.Systems {

    [UpdateInGroup(typeof(GameGroup))]
    public class DestoryBulletSystem : ComponentSystem
    {

        protected override void OnUpdate(){
            var manager = World.Active.EntityManager;
            Entities.ForEach(( Entity Entity, ref ECS.Components.Bullet bullet, ref Translation pos )=>{
                if( pos.Value.y < -7.0f ){
                    manager.AddComponent<ECS.Components.DestoryData>(Entity);

                    var entity = manager.CreateEntity(typeof(ECS.Components.ChangeGameStateRequester), typeof(ECS.Components.GameEntity) );
                    manager.SetComponentData(entity, new ECS.Components.ChangeGameStateRequester{ isRequest = 1, state = ECS.Components.GameState.Finish });
                }
            });
        }

    }
}