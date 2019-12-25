using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField]
    float _speed = 5.0f;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem){
        var player = new ECS.Components.Player{ speed = _speed };
        dstManager.AddComponentData(entity, player);
        dstManager.AddComponentData(entity, new ECS.Components.Velocity());
        dstManager.AddComponentData(entity, new ECS.Components.InputInfomation());
    }
}
