using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private Transform newParentObj;
        #endregion

        #region Private Variables

        private CD_PoolObjects _data;

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetObjectData();
            SpawnParentObjects();
            
        }

        private CD_PoolObjects GetObjectData() => Resources.Load<CD_PoolObjects>("Data/CD_PoolObjects");
       


        private void SpawnParentObjects()
        {
            for (int i = 0; i < _data.data.Count; i++)
            {
                var data = _data.data[i];
                var newObject = new GameObject();
                newObject.name = data.Color.ToString();
                newObject.transform.parent = transform;
                
                foreach (var colorObject in data.ColorObjects)
                {
                    var newGameObject = new GameObject();
                    newGameObject.name = colorObject.Type.ToString();
                    newGameObject.transform.parent = newObject.transform;
                    for (int k = 0; k < colorObject.Count; k++)
                    {
                        var obj = Instantiate(colorObject.obj, newGameObject.transform);
                        obj.SetActive(false);
                    }
                }
                

            }
        }

        [Button("Get Color Object")]
        private void GetColorObject( TypeOfColorEnum typeOfColor, ColorEnum color,int count)
        {
            var colorObj = transform.GetChild((int)color).gameObject;
            var typeOfObj = colorObj.transform.GetChild((int)typeOfColor).gameObject;
            for (int i = 0; i < count; i++)
            {
                var newObj = typeOfObj.transform.GetChild(i).gameObject;
                newObj.transform.parent = newParentObj;
            }

        }

       
    }
}