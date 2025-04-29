using System;
using System.Collections.Generic;
using RunTime.Enums;
using UnityEngine;

namespace RunTime.Data.ValueObject
{
    [Serializable]
    public struct PoolObject
    {
        public ColorEnum Color;
        public List<ColorObject> ColorObjects;
    }
    
  
    [Serializable]
    public struct ColorObject
    {
        public int Count;
        public TypeOfColorEnum Type;
        public GameObject obj;
    }
}