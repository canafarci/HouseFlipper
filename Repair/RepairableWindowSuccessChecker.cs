using EasyMobile;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableWindowSuccessChecker : MonoBehaviour, ISuccessChecker
{
    [SerializeField] int _actionsLeftToDo;
    RepairStateSwitcher _repairStateSwitcher;
    [SerializeField] GameObject _fx;
    [Header("Game Analytics")]
    [SerializeField] string _stageName;

    private void Awake()
    {
        _repairStateSwitcher = FindObjectOfType<RepairStateSwitcher>();
    }

    public void OnActionSuccessful()
    {
        _actionsLeftToDo--;

        if (_actionsLeftToDo == 0)
        {
            //STAGE OVER
            if (_fx != null)
                _fx.SetActive(true);
            print("stage over");
            _repairStateSwitcher.SwitchState(RepairStage.Reset, null);
            this.enabled = false;
            //SEND GA EVENT
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Repair_Scene", _stageName);


            
        }
    }
}
