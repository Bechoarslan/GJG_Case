using System.Numerics;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

namespace RunTime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        
        public  UnityAction<Vector2> onPlayerClickedToBlast = delegate {  };
        public UnityAction<bool> onIsPlayerReadyToClick = delegate {  };
    }
}