using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ECS.Systems {

    [UpdateInGroup(typeof(GameGroup))]
    [UpdateAfter(typeof(BulletCollisionSystem))]
    [DisableAutoCreation]
    public class UpdateScoreSystem : ComponentSystem
    {
        readonly Text _ScoreText;

        EntityQuery _query;

        public UpdateScoreSystem(Text text){
            _ScoreText = text;
        }

        protected override void OnUpdate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<ECS.Components.AddScoreData>());
            var manager = World.Active.EntityManager;
            Entities.ForEach(( ref ECS.Components.Score score )=>{
                score.value += _query.CalculateEntityCount();
                _ScoreText.text = score.value.ToString();
            });
        }
    }
}