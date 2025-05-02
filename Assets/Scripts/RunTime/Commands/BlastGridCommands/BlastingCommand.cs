using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnityObject;
using RunTime.Enums;
using RunTime.Keys;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.BlastGridCommands
{
    public class BlastingCommand
    {
        private Dictionary<Vector2, BlastKeys> _blastDictionary;
        private FindSameColorCommand _findSameColorCommand;
        private CD_BlastData _blastData;
        private ChangeBlastTypeCommand _changeBlastTypeCommand;
        private Transform _collectedBlats;
        public BlastingCommand(Dictionary<Vector2, BlastKeys> blastDictionary,
            FindSameColorCommand findSameColorCommand, ChangeBlastTypeCommand changeBlastTypeCommand,
            CD_BlastData blastData, Transform collectedBlats)
        {
            _blastDictionary = blastDictionary;
            _findSameColorCommand = findSameColorCommand;
            _changeBlastTypeCommand = changeBlastTypeCommand;
            _blastData = blastData;
            _collectedBlats = collectedBlats;
            
        }

        public void Execute(Vector2 pos)
        {
            
            var blastList = _findSameColorCommand.Execute(pos);
            if (blastList.Count < 2) return;
            foreach (var item in blastList)
            {
                if (!_blastDictionary.ContainsKey(item)) return;
                var blastObj = _blastDictionary[item];
                var blastColor = blastObj.color;
                var blastType = blastObj.type;
                var blastGameObject = blastObj.obj;
                _blastDictionary.Remove(item);
                var spriteRenderer = blastGameObject.GetComponent<SpriteRenderer>();
                Sequence blastSequence = DOTween.Sequence();
                blastSequence.Append(blastGameObject.transform.DOScale(1.3f, 0.15f))
                    .Join(spriteRenderer.DOFade(0f, 0.15f))
                    .OnComplete(() =>
                    {
                        blastGameObject.transform.localScale = Vector3.one;
                        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

                        PoolSignals.Instance.onSendBlastObjectToPool?.Invoke(blastType, blastColor, blastGameObject);
                    });
              

                
            }

            CheckForDropDown();
             
        }

        private void CheckForDropDown()
        {
            var data = _blastData.data[_blastData.Level];
            for (int x = 0; x < data.Rows; x++)
            {
                for (int y = 0; y < data.Columns; y++)
                {
                    Vector2 currentPos = new Vector2(x, y);
                    if (!_blastDictionary.ContainsKey(currentPos))
                    {
                        bool filled = false;
                        
                        for (int upperY = y + 1; upperY < data.Columns; upperY++)
                        {
                            Vector2 upperPos = new Vector2(x, upperY);
                            if (_blastDictionary.ContainsKey(upperPos))
                            {
                                var fallingBlock = _blastDictionary[upperPos];
                                _blastDictionary[currentPos] = fallingBlock;
                                _blastDictionary.Remove(upperPos);

                                Vector3 targetPos = new Vector3(x * 2.2f, y * 2.2f, 0);
                                fallingBlock.obj.GetComponent<SpriteRenderer>().sortingOrder = y;
                                fallingBlock.obj.transform.DOLocalMove(targetPos, 0.2f).SetEase(Ease.OutQuad);

                                filled = true;
                                break;
                            }
                        }

                        
                        if (!filled)
                        {
                          
                            var colorData = data.Colors;
                            var color = colorData[Random.Range(0, colorData.Count)];
                            GameObject newBlock = PoolSignals.Instance.onGetBlastObject?.Invoke(TypeOfBlastEnum.Default, color, 1, _collectedBlats);
                            

                            Vector3 spawnPos = new Vector3(x * 2.2f, (data.Columns + 2) * 2.2f, 0);
                            
                           
                            Vector3 targetPos = new Vector3(x * 2.2f, y * 2.2f, 0);
                            newBlock.transform.localPosition = spawnPos;
                            newBlock.transform.localScale = Vector3.one;
                            newBlock.GetComponent<SpriteRenderer>().sortingOrder = y;
                            newBlock.SetActive(true);
                            _blastDictionary[currentPos] = new BlastKeys(newBlock, color, TypeOfBlastEnum.Default);
                            newBlock.transform.DOLocalMove(targetPos, 0.25f).SetEase(Ease.OutBounce).onComplete += () =>
                            {
                                _changeBlastTypeCommand.Execute();
                            };

                            
                        }
                    }
                }
                
            }
            
        }
    }
        }
        

        
    
