using System;
using RunTime.Enums;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<TypeOfBlastEnum,BlastColorEnum,int,Transform,GameObject> onGetBlastObject = delegate { return null; };
        public UnityAction<TypeOfBlastEnum, BlastColorEnum, GameObject> onSendBlastObjectToPool = delegate { };
    }
}