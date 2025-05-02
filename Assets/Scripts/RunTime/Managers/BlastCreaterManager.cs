using System;
using RunTime.Controllers;
using RunTime.Data.UnityObject;
using RunTime.Signals;
using UnityEngine;

public class BlastCreaterManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private BlastGridController blastGridController;

    #endregion

    #region Private Variables

    private CD_BlastData _blastData;
    private CD_BlastingEffect _blastingEffect;

    #endregion

    #endregion


    private void Awake()
    {
        _blastData = GetBlastData();
        _blastingEffect = GetEffectData();
        SendDataToControllers(_blastData,_blastingEffect);
    }

    private CD_BlastingEffect GetEffectData() => Resources.Load<CD_BlastingEffect>("Data/CD_BlastingEffect");
    


    private CD_BlastData GetBlastData() => Resources.Load<CD_BlastData>("Data/CD_BlastData");
    
    private void SendDataToControllers(CD_BlastData blastData,CD_BlastingEffect blastingEffect)
    {
        blastGridController.GetBlastData(blastData);
        blastGridController.GetEffectData(blastingEffect);
        
        
    }


    private void OnEnable()
    {
        OnSubscribeEvents();
    }

    private void OnSubscribeEvents()
    {
        InputSignals.Instance.onPlayerClickedToBlast += blastGridController.OnPlayerClickedToBlast;
    }
    
    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onPlayerClickedToBlast -= blastGridController.OnPlayerClickedToBlast;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    
    
    
}
