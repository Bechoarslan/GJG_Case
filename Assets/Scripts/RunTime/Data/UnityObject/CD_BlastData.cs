using System.Collections.Generic;
using RunTime.Data.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    
    [CreateAssetMenu(fileName = "CD_BlastData", menuName = "BlastGame/CD_BlastData", order = 0)]
    public class CD_BlastData : ScriptableObject
    {
        public int Level;
        public List<BlastData> data;
    }
}