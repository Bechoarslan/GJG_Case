using RunTime.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BlastingEffect", menuName = "BlastGame/CD_BlastingEffect", order = 0)]
    public class CD_BlastingEffect : ScriptableObject
    {
        public BlastingEffectData data;
    }
}