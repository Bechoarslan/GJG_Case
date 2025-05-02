using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using RunTime.Data.UnityObject;
using RunTime.Enums;
using RunTime.Keys;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.BlastGridCommands
{
    public class ChangeBlastTypeCommand
    {
       
        private Dictionary<Vector2, BlastKeys> _blastDictionary;
        private Transform _collectedBlats;
        private GameStartCheckAllTheGridCommand _gameStartCheckAllTheGridCommand;

        public ChangeBlastTypeCommand(GameStartCheckAllTheGridCommand gameStartCheckAllTheGridCommand,
            Dictionary<Vector2, BlastKeys> blastDictionary
            , Transform collectedBlats)
        {
            _blastDictionary = blastDictionary;
            _gameStartCheckAllTheGridCommand = gameStartCheckAllTheGridCommand;
            _collectedBlats = collectedBlats;
        }


        public void Execute()
        {
            var matchedGroups = _gameStartCheckAllTheGridCommand.Execute();
            
            foreach (var matched in matchedGroups)
            {
                if (matched.Count < 5) continue;

                // Change the type of blast depending on the matched count
                TypeOfBlastEnum blastType = GetBlastType(matched.Count);
                ChangeTheItems(matched, blastType);
            }
           
            Debug.LogWarning(matchedGroups.Count);
        }

        private TypeOfBlastEnum GetBlastType(int matchedCount)
        {
            if (matchedCount >= 10) return TypeOfBlastEnum.Lolly;
            if (matchedCount >= 7) return TypeOfBlastEnum.Fire;
            if (matchedCount >= 5) return TypeOfBlastEnum.Bomb;

            return TypeOfBlastEnum.Default; // 5'ten az ise varsayılan tip
        }


        public void ChangeTheItems(List<Vector2> matched, TypeOfBlastEnum type)
        {
            
            foreach (var item in matched)
            {
                var matchedObj = _blastDictionary[item];
                var newObj = PoolSignals.Instance.onGetBlastObject?.Invoke(type, matchedObj.color, 1, _collectedBlats);

                
                newObj.transform.localPosition = matchedObj.obj.transform.localPosition;
                newObj.transform.localScale = matchedObj.obj.transform.localScale;
                newObj.GetComponent<SpriteRenderer>().sortingOrder = matchedObj.obj.GetComponent<SpriteRenderer>().sortingOrder;
                newObj.SetActive(true);  

                
                _blastDictionary[item] = new BlastKeys(newObj, matchedObj.color,type);

                
                PoolSignals.Instance.onSendBlastObjectToPool?.Invoke(TypeOfBlastEnum.Default, matchedObj.color, matchedObj.obj);
            }
        }

    }
}