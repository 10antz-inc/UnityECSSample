using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ECS.Systems {

    [UpdateInGroup(typeof(PostGameGroup))]
    [UpdateBefore(typeof(DestroyEntitySystem))]
    public class ResolveChangeGameStateRequestSystem : ComponentSystem
    {

        protected override void OnUpdate(){
            var manager = World.Active.EntityManager;
            var requestState = ECS.Components.GameState.None;
            Entities.ForEach(( Entity entity, ref ECS.Components.ChangeGameStateRequester requester )=>{
                if( requester.isRequest == 1 ){
                    requestState = requester.state;
                    requester.isRequest = 0;
                }
            });

            if( requestState != ECS.Components.GameState.None ){
                var state = GetSingleton<ECS.Components.GameStateData>();
                var prevScene = state.prev;
                state.next = requestState;
                SetSingleton<ECS.Components.GameStateData>(state);
            }

        }
    }
}