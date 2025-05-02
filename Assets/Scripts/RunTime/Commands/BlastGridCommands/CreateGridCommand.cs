using System;
using System.Collections.Generic;
using RunTime.Data.UnityObject;
using RunTime.Enums;
using RunTime.Keys;
using RunTime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunTime.Commands.BlastGridCommands
{
    public class CreateGridCommand
    {
        private CD_BlastData _blastData;
        private Transform _collectedBlats;
        private Dictionary<Vector2, BlastKeys> _blastDictionary;
        private ChangeBlastTypeCommand _changeBlastTypeCommand;
        private GameStartCheckAllTheGridCommand _gameStartCheckAllTheGridCommand;
        public CreateGridCommand(CD_BlastData blastData, ref Transform collectedBlats,
            Dictionary<Vector2, BlastKeys> blastDictionary, ChangeBlastTypeCommand changeBlastTypeCommand,
            GameStartCheckAllTheGridCommand gameStartCheckAllTheGridCommand)
        {
            _blastData  = blastData;
            _collectedBlats = collectedBlats;
            _blastDictionary = blastDictionary;
            _changeBlastTypeCommand = changeBlastTypeCommand;
            _gameStartCheckAllTheGridCommand = gameStartCheckAllTheGridCommand;
        }

        public void Execute()
        {
            var blastData = _blastData.data[_blastData.Level];
            var scaleOfBlast = blastData.Rows * blastData.Columns;
            var colorData = blastData.Colors;
            for (int i = 0; i < _blastData.data[_blastData.Level].Rows; i++)
            {
                for (int j = 0; j < _blastData.data[_blastData.Level].Columns; j++)
                {
                    var getRandomColor = colorData[Random.Range(0, colorData.Count)];
                    var newObj = PoolSignals.Instance.onGetBlastObject?.Invoke(TypeOfBlastEnum.Default, getRandomColor,
                        1, _collectedBlats);
                    newObj.SetActive(true);
                    newObj.GetComponent<SpriteRenderer>().sortingOrder = j;
                    newObj.transform.position = new Vector3(i * 2.2f, j * 2.2f, 0);
                 
                    _blastDictionary[new Vector2(i , j )] = new BlastKeys(newObj,getRandomColor, TypeOfBlastEnum.Default);




                }
            }

            var gridWith = (blastData.Columns - 1) * 2.2f;
            var gridHeight = (blastData.Rows - 1) * 2.2f;
            var scaleX = 10 / gridWith;
            var scaleY = 10 / gridHeight;
            var uniformScale = Mathf.Min(scaleX, scaleY);
            if (blastData.Columns * blastData.Rows < 10)
            {
                uniformScale = 0.5f;
            }
            _collectedBlats.transform.localScale =
                new Vector3(uniformScale , uniformScale, 0);
       
           
            
            _changeBlastTypeCommand.Execute();

        }
    }
}