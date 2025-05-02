using System;
using RunTime.Enums;
using UnityEngine;

namespace RunTime.Keys
{
    [Serializable]
    public class BlastKeys
    {
     
        public GameObject obj;
        public BlastColorEnum color;
        
        public BlastKeys( GameObject gameObject, BlastColorEnum blastColor)
        {

            obj = gameObject;
            color = blastColor;


        }

    }

   
    
}