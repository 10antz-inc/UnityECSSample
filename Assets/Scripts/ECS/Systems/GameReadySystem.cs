using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS.Systems {

    [UpdateInGroup(typeof(GameGroup))]
    public class GameReadySystem : ComponentSystem
    {

        protected override void OnCreate(){

        }


        protected override void OnUpdate(){

            Entities.ForEach(( ref ECS.Components.GameReady game, ref ECS.Components.InputInfomation info, ref ECS.Components.ChangeGameStateRequester stateRequester )=>{
                if( info.isClick == 1 ) {
                    // EntityManager.DestroyEntity(entity);
                    // var state = GetSingleton<ECS.Components.GameStateData>();
                    // state.prev = state.current;
                    // state.next = ECS.Components.GameState.Game;
                    // SetSingleton<ECS.Components.GameStateData>(state);
                    // var creator = new GameCreator();
                    // creator.Create();
                    stateRequester.isRequest = 1;
                    stateRequester.state = ECS.Components.GameState.Game;
                }
            });

        }
    }
}