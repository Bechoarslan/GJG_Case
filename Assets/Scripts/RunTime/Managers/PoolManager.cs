using System;
using RunTime.Commands.PoolCommands;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Enums;
using RunTime.Signals;
using Sirenix.OdinInspector;
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
        private SpawnBlastObjectCommand _spawnBlastObjectCommand;

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetObjectData();
            _spawnBlastObjectCommand = new SpawnBlastObjectCommand(_data,transform);
            _spawnBlastObjectCommand.Execute();
        }

        private CD_PoolObjects GetObjectData() => Resources.Load<CD_PoolObjects>("Data/CD_PoolObjects");
        
        private void OnEnable()
        {
            OnSubscribeEvents();
        }

        private void OnSubscribeEvents()
        {
            PoolSignals.Instance.onGetBlastObject += GetBlastObject;
        }

        private void OnUnsubscribeEvents()
        {
            PoolSignals.Instance.onGetBlastObject -= GetBlastObject;
        }

        private void OnDisable()
        {
            OnUnsubscribeEvents();
        }
        
        private GameObject GetBlastObject(TypeOfBlastEnum typeOfBlast, BlastColorEnum blastColor,int count,Transform newParent)
        {
            
            var colorObj = transform.GetChild((int)blastColor).gameObject;
            var typeOfObj = colorObj.transform.GetChild((int)typeOfBlast).gameObject;
            if (typeOfObj.transform.childCount > 0)
            {
                var newObj = typeOfObj.transform.GetChild(typeOfObj.transform.childCount - 1).gameObject;
                Debug.LogWarning(newObj.name);
                newObj.transform.parent = newParent;
                return newObj;
            }
            else
            {
                var newObj = Instantiate(_data.data[(int)blastColor].ColorObjects[(int)typeOfBlast].obj, newParent);
                newObj.SetActive(false);
                return newObj;
            }

            
        }

        private void SendBlastObject()
        {
            
        } 

       
    }
}