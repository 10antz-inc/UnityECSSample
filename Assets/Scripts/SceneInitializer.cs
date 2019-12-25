using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;


public class SceneInitializer : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text _titleText = null;
    [SerializeField]
    UnityEngine.UI.Text _scoreText = null;

    [SerializeField]
    Mesh _bulletMesh = null;

    [SerializeField]
    Material _bulleMaterial = null;


    void Start() {
        // var world = new World("TestGame");
        // World.Active = world;
        var world = World.Active;

        var gameGroup = world.GetOrCreateSystem<GameGroup>();
        gameGroup.AddSystemToUpdateList(world.CreateSystem<ECS.Systems.BulletGenerateSystem>(_bulletMesh, _bulleMaterial));
        gameGroup.AddSystemToUpdateList(world.CreateSystem<ECS.Systems.UpdateScoreSystem>(_scoreText));
        gameGroup.SortSystemUpdateList();

        var postGameGroup = world.GetOrCreateSystem<PostGameGroup>();
        postGameGroup.AddSystemToUpdateList(world.CreateSystem<ECS.Systems.TitleTextSystem>(_titleText));

        postGameGroup.SortSystemUpdateList();

        ScriptBehaviourUpdateOrder.UpdatePlayerLoop(world);

        ExecuteMonoBehaviourEntityCreator();
        ExecuteEntityCreator();
    }


    void ExecuteEntityCreator(){
        var creatorTypes = Assembly.GetExecutingAssembly().GetTypes().Where(c => c.GetInterfaces().Any(t => t == typeof(IInitialEntityCreator)));
        foreach(var creatorType in creatorTypes){
            var creator = Activator.CreateInstance(creatorType) as IInitialEntityCreator;
            creator.Create();
        }
    }

    void ExecuteMonoBehaviourEntityCreator(){
        foreach (var n in GameObject.FindObjectsOfType<Component>()) {
            var component = n as IInitialMonoBehaviourEntityCreator;
            if (component != null) {
                component.Create();
            }
        }

    }
}
