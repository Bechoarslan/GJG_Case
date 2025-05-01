using System;
using RunTime.Controllers;
using RunTime.Data.UnityObject;
using UnityEngine;

public class BlastCreaterManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private BlastGridController blastGridController;

    #endregion

    #region Private Variables

    private CD_BlastData _blastData;

    #endregion

    #endregion


    private void Awake()
    {
        _blastData = GetBlastData();
        SendDataToControllers(_blastData);
    }
    

    private CD_BlastData GetBlastData() => Resources.Load<CD_BlastData>("Data/CD_BlastData");
    
    private void SendDataToControllers(CD_BlastData blastData)
    {
        blastGridController.GetBlastData(blastData);
    }
}
