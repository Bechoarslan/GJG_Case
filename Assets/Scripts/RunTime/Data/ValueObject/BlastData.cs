using System;
using System.Collections.Generic;
using RunTime.Enums;
using Unity.VisualScripting;

namespace RunTime.Data.ValueObject
{
    [Serializable]
    public struct BlastData
    {
        public int Rows;
        public int Columns;
        public List<BlastColorEnum> Colors;
    }

    
    
}