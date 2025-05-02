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
        public TypeOfBlastEnum type;
        
        public BlastKeys( GameObject gameObject, BlastColorEnum blastColor, TypeOfBlastEnum type)
        {

            obj = gameObject;
            color = blastColor;
            this.type = type;



        }

    }

   
    
}