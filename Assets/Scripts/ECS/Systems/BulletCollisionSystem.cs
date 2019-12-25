using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ECS.Systems {

    [UpdateInGroup(typeof(GameGroup))]
    public class BulletCollisionSystem : ComponentSystem
    {

        protected override void OnUpdate(){
            var manager = World.Active.EntityManager;
            var player =  GetSingletonEntity<ECS.Components.Player>();
            var playerPosition = manager.GetComponentData<Translation>(player);
            Entities.ForEach(( Entity Entity, ref ECS.Components.Bullet bullet, ref Translation pos, ref ECS.Components.ToPlayerCollision col )=>{
                float3 toVec = playerPosition.Value - pos.Value;
                var toVec2 = new Vector2( toVec.x, toVec.y);
                if( toVec2.SqrMagnitude() < 1.0f ){
                    manager.AddComponent<ECS.Components.AddScoreData>(Entity);
                    manager.AddComponent<ECS.Components.DestoryData>(Entity);
                }
            });
        }

    }
}