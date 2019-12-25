using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS.Systems {

    [UpdateInGroup(typeof(GameGroup))]
    public class GameFinish : ComponentSystem
    {

        protected override void OnCreate(){

        }


        protected override void OnUpdate(){

            Entities.ForEach(( ref ECS.Components.GameFinish game, ref ECS.Components.InputInfomation info, ref ECS.Components.ChangeGameStateRequester stateRequester )=>{
                if( info.isClick == 1 ) {
                    stateRequester.isRequest = 1;
                    stateRequester.state = ECS.Components.GameState.Ready;
                }
            });

        }
    }
}