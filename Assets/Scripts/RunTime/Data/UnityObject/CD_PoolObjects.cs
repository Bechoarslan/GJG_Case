using System.Collections.Generic;
using RunTime.Data.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PoolObjects", menuName = "BlastGame/CD_PoolObjects", order = 0)]
    public class CD_PoolObjects : ScriptableObject
    {
        public List<PoolObject> data;
    }
}