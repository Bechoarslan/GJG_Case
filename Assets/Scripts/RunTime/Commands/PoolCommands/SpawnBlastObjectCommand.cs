using RunTime.Data.UnityObject;
using UnityEngine;

namespace RunTime.Commands.PoolCommands
{
    public class SpawnBlastObjectCommand
    {
        private CD_PoolObjects _data;
        private Transform transform;
        public SpawnBlastObjectCommand(CD_PoolObjects data,Transform parent)
        {
            _data = data;
            transform = parent;
        }

        public void Execute()
        {
            for (int i = 0; i < _data.data.Count ; i++)
            {
                var data = _data.data[i];
                var newObject = new GameObject();
                newObject.name = data.blastColor.ToString();
                newObject.transform.parent = transform;
                
                foreach (var colorObject in data.ColorObjects)
                {
                    var newGameObject = new GameObject();
                    newGameObject.name = colorObject.Type.ToString();
                    newGameObject.transform.parent = newObject.transform;
                    for (int k = 0; k < colorObject.Count; k++)
                    {
                        var obj = Object.Instantiate(colorObject.obj, newGameObject.transform);
                        obj.SetActive(false);
                    }
                }
                

            }
        }
    }
}