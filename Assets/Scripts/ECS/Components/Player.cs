using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


namespace ECS.Components {
    public struct Player : IComponentData {
        public float speed;
    }
}