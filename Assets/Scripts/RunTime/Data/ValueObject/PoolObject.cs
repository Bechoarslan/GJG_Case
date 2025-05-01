using System;
using System.Collections.Generic;
using RunTime.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Data.ValueObject
{
    [Serializable]
    public struct PoolObject
    {
        [FormerlySerializedAs("Color")] public BlastColorEnum blastColor;
        public List<ColorObject> ColorObjects;
    }
    
  
    [Serializable]
    public struct ColorObject
    {
        public int Count;
        public TypeOfBlastEnum Type;
        public GameObject obj;
    }
}