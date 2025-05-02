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
        private CD_BlastData _blastData;
        private Dictionary<Vector2, BlastKeys> _blastDictionary;
        private Transform _collectedBlats;

        public ChangeBlastTypeCommand(CD_BlastData blastData, Dictionary<Vector2, BlastKeys> blastDictionary
             , Transform collectedBlats)
        {
            _blastData = blastData;
            _blastDictionary = blastDictionary;
            
            _collectedBlats = collectedBlats;
        }


        public void Execute(List<List<Vector2>> matchedGroups)
        {
            
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

                Debug.LogWarning("matchedObj: " + matchedObj.obj.GetInstanceID());
                // Yeni objeyi eski objenin pozisyonuna yerleştir
                newObj.transform.localPosition = matchedObj.obj.transform.localPosition;
                newObj.transform.localScale = matchedObj.obj.transform.localScale;
                newObj.GetComponent<SpriteRenderer>().sortingOrder = matchedObj.obj.GetComponent<SpriteRenderer>().sortingOrder;
                newObj.SetActive(true); // Yeni objeyi aktif yap

                // Dictionary'i güncelle
                _blastDictionary[item] = new BlastKeys(newObj, matchedObj.color);

                Debug.LogWarning(newObj.GetInstanceID());
                Debug.LogWarning(_blastDictionary[item].obj.GetInstanceID());
                // Eski objeyi havuza geri gönder
                PoolSignals.Instance.onSendBlastObjectToPool?.Invoke(TypeOfBlastEnum.Default, matchedObj.color, matchedObj.obj);
            }
        }

    }
}