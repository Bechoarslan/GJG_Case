using System;
using System.Collections.Generic;
using System.Linq;
using RunTime.Data.UnityObject;
using RunTime.Enums;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunTime.Controllers
{
    public class BlastGridController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform collectedBlats;

        #endregion

        #region Private Variables

        private CD_BlastData _blastData;


        private Dictionary<Vector2, GameObject> _blastDictionary = new();

        #endregion

        #endregion

        public void GetBlastData(CD_BlastData blastData) => _blastData = blastData;
        

        [Button("CreateGrid")]
        private void CreateGrid()
        {
            var scaleOfBlast = _blastData.data[_blastData.Level].Rows * _blastData.data[_blastData.Level].Columns;
            var colorData = _blastData.data[_blastData.Level].Colors;
            for (int i = 0; i < _blastData.data[_blastData.Level].Rows; i++)
            {
                for (int j = 0; j < _blastData.data[_blastData.Level].Columns; j++)
                {
                    var getRandomColor = colorData[Random.Range(0, colorData.Count)];
                    var newObj = PoolSignals.Instance.onGetBlastObject?.Invoke(TypeOfBlastEnum.Default, getRandomColor,
                        1, collectedBlats);
                    newObj.SetActive(true);
                    newObj.GetComponent<SpriteRenderer>().sortingOrder = j;
                    newObj.transform.position = new Vector3(i * 2.2f, j * 2.2f, 0);
                 
                    _blastDictionary[new Vector2(i, j)] = newObj;




                }
            }

            collectedBlats.transform.localScale =
                new Vector3((scaleOfBlast / 110f), (scaleOfBlast / 110f), 0);


        }

        private List<Vector2> FindSameColor(Vector2 startPos)
        {
            List<Vector2> matched = new();
            Stack<Vector2> stack = new();
            HashSet<Vector2> visited = new();

            var startObj = _blastDictionary[startPos];
            var startColor = startObj.tag;
            stack.Push(startPos);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);
                var currentObj = _blastDictionary[current];
                var currentColor = currentObj.tag;
                if (currentColor != startColor)
                    continue;
                matched.Add(current);

                Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
                foreach (var dir in directions)
                {
                    Vector2 neighbor = current + dir;
                    if (_blastDictionary.ContainsKey(neighbor))
                        stack.Push(neighbor);

                }
            }

            return matched;
        }

        [Button("Explode Objects")]
        private void TryExplode(Vector2 pos)
        {
            var matched = FindSameColor(pos);
            if (matched.Count >= 2)
            {
                foreach (var item in matched)
                {
                    var blastKey = _blastDictionary[item];

                    if (Enum.TryParse(blastKey.tag, ignoreCase: true, out BlastColorEnum color))
                    {
                        Debug.Log("This object is of color: " + color);
                        
                       var newObj = PoolSignals.Instance.onGetBlastObject?.Invoke(TypeOfBlastEnum.Bomb, color, 1,
                            blastKey.transform);
                        newObj.transform.position = blastKey.transform.position;
                        
                        blastKey.SetActive(false);
                        newObj.SetActive(true);
                        _blastDictionary.Remove(item);
                    }
                    else
                    {
                        Debug.LogWarning("Tag does not match any ColorType!");
                    }
                    
                }
            }

        }

    }

}