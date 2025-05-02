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
      

        #endregion

        #region Private Variables

        private CD_BlastData _blastData;
        private CD_BlastingEffect _blastingEffect;
        private CreateGridCommand _createGridCommand;
        private FindSameColorCommand _findSameColorCommand;
        private ChangeBlastTypeCommand _changeBlastTypeCommand;
        private GameStartCheckAllTheGridCommand _gameStartCheckAllTheGridCommand;
        private BlastingCommand _blastingCommand;

        [DictionaryDrawerSettings(KeyLabel = "Vector2", ValueLabel = "Item")] [SerializeField]
        private Dictionary<Vector2, BlastKeys> _blastDictionary = new();

        #endregion

        #endregion

        private void Awake()
        {
            _findSameColorCommand = new FindSameColorCommand(_blastDictionary);
            _gameStartCheckAllTheGridCommand = new GameStartCheckAllTheGridCommand(_blastDictionary);
            _changeBlastTypeCommand = new ChangeBlastTypeCommand(_gameStartCheckAllTheGridCommand, _blastDictionary,
                collectedBlats);
            _createGridCommand = new CreateGridCommand(_blastData, ref collectedBlats, _blastDictionary,
                _changeBlastTypeCommand,_gameStartCheckAllTheGridCommand);
            _blastingCommand = new BlastingCommand(_blastDictionary,_findSameColorCommand,_changeBlastTypeCommand,_blastData,collectedBlats);



        }

        [Button]
        private void CheckStatu()
        {
            _changeBlastTypeCommand.Execute();
        }
        private void Start()
        {
          
            _createGridCommand.Execute();
        }

       

        public void GetBlastData(CD_BlastData blastData) => _blastData = blastData;
        public void GetEffectData(CD_BlastingEffect blastingEffect) => _blastingEffect = blastingEffect;
        
        

        public void OnPlayerClickedToBlast(Vector2 pos)
        {
            var newPos = new Vector2(pos.x / 2.2f, pos.y / 2.2f);
            _blastingCommand.Execute(newPos);


        }

        
    }
    }

