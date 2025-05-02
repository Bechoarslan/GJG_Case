using System;
using System.Collections.Generic;
using System.Linq;
using RunTime.Commands.BlastGridCommands;
using RunTime.Data.UnityObject;
using RunTime.Enums;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace RunTime.Controllers
{
    public class BlastGridController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform collectedBlats;
        [SerializeField] private Camera camera;

        #endregion

        #region Private Variables

        private CD_BlastData _blastData;
        private CreateGridCommand _createGridCommand;
        private FindSameColorCommand _findSameColorCommand;
        private ChangeBlastTypeCommand _changeBlastTypeCommand;
        private GameStartCheckAllTheGridCommand _gameStartCheckAllTheGridCommand;


        [DictionaryDrawerSettings(KeyLabel = "Vector2", ValueLabel = "Item")] [SerializeField]
        private Dictionary<Vector2, BlastKeys> _blastDictionary = new();

        #endregion

        #endregion

        private void Awake()
        {
            _findSameColorCommand = new FindSameColorCommand(_blastDictionary);
            _gameStartCheckAllTheGridCommand = new GameStartCheckAllTheGridCommand(_blastDictionary);
            _changeBlastTypeCommand = new ChangeBlastTypeCommand(_blastData, _blastDictionary,
                collectedBlats);
            _createGridCommand = new CreateGridCommand(_blastData, ref collectedBlats, _blastDictionary,
                _changeBlastTypeCommand,_gameStartCheckAllTheGridCommand);



        }

        private void Start()
        {
            _createGridCommand.Execute();
        }

        public void GetBlastData(CD_BlastData blastData) => _blastData = blastData;


        public void OnPlayerClickedToBlast(Vector2 pos)
        {
            var newPos = new Vector2(pos.x / 2.2f, pos.y / 2.2f);

            if (_blastDictionary.ContainsKey(newPos))
            {
                var listOfBlats = _findSameColorCommand.Execute(newPos);
                foreach (var item in listOfBlats)
                {
                    Debug.LogWarning("Blasting");
                      var blastKey =_blastDictionary[item];
                        var blastObject = blastKey.obj;
                        var blastColor = blastKey.color;

                        if (blastObject != null)
                        {

                            _blastDictionary.Remove(item);
                            blastObject.SetActive(false);
                        }
                }
            }


        }
        }
    }

