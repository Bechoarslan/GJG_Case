using System;
using RunTime.Enums;
using UnityEngine;

namespace RunTime.Keys
{
    [Serializable]
    public class BlastKeys
    {
     
        public GameObject obj;
        public string color;
        
        public BlastKeys( GameObject gameObject, string blastColor)
        {
            
            var obj = gameObject;
            var color = blastColor;

            // Use the variables as needed
            Debug.Log($" Object: {obj.name}, Color: {color}");
        }

    }

   
    
}