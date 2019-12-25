using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS.Systems {

    [UpdateInGroup(typeof(PreviousGameGroup))]
    public class UpdateInputInfomationSystem : ComponentSystem
    {
        protected override void OnUpdate(){
            var isClick = Input.GetMouseButton(0);
            var inputState = ECS.Components.InputState.None;

            if( UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow)){
                inputState = ECS.Components.InputState.Left;
            } else if(UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow)) {
                inputState = ECS.Components.InputState.Right;
            }


            Entities.ForEach(( ref ECS.Components.InputInfomation info )=>{
                if( isClick ) {
                    if( info.isReleased == 1 ) {
                        info.isClick = 1;
                    }

                } else{
                    info.isClick = 0;
                    info.isReleased = 1;
                }

                info.state = inputState;
            });

        }
    }
}