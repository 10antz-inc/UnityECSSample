﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ECS.Components {
    public struct Velocity : IComponentData {
        public float3 value;
    }
}