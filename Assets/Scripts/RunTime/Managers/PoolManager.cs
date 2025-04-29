using RunTime.Data.UnityObject;
using UnityEngine;

namespace RunTime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

        private CD_PoolObjects _data;

        #endregion

        #endregion
        private void Awake()
        {
            
            SpawnBlastObjects();
        }

       
        

        private void SpawnBlastObjects()
        {
            
        }
    }
}