using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ECS.Systems {

    [UpdateInGroup(typeof(PostGameGroup))]
    [UpdateAfter(typeof(ResolveChangeGameStateRequestSystem))]
    public class ChangeGameStateSystem : ComponentSystem
    {

        protected override void OnUpdate(){
            var manager = World.Active.EntityManager;
            var isExecuteChange = false;    //多重で変更処理が走らないように、スマートじゃないけどいい方法が思いつかない
            Entities.ForEach( ( ref ECS.Components.GameStateData state )=>{
                if( !isExecuteChange && state.prev != state.next ) {
                    ChangeGameState(ref state);
                    state.prev = state.next;
                    state.current = state.next;
                    isExecuteChange = true;
                }
            });

        }

        void ChangeGameState( ref ECS.Components.GameStateData state ){
            DestoryPrevSceneEntity(state.prev);
            CreateSceneEntity(state.next);
        }

        void DestoryPrevSceneEntity( ECS.Components.GameState state ){
            ComponentType type = null;
            var manager = World.Active.EntityManager;
            switch(state){
                case ECS.Components.GameState.Ready:
                type = ComponentType.ReadOnly<ECS.Components.GameReadyEntity>();
                break;

                case ECS.Components.GameState.Game:
                type = ComponentType.ReadOnly<ECS.Components.GameEntity>();
                break;

                case ECS.Components.GameState.Finish:
                type = ComponentType.ReadOnly<ECS.Components.GameFinishEntity>();
                break;

                default:
                return;
            }

            Entities.WithAny(type).ForEach((Entity entity)=>{
                manager.DestroyEntity(entity);
            });
        }


        void CreateSceneEntity( ECS.Components.GameState state ){
            IEntityCreator creator = null;
            switch(state){
                case ECS.Components.GameState.Ready:
                creator = new GameReadyCreator();
                break;

                case ECS.Components.GameState.Game:
                creator = new GameCreator();
                break;

                case ECS.Components.GameState.Finish:
                creator = new GameFinishCreator();
                break;
                default:
                return;
            }

            creator.Create();
        }

    }
}