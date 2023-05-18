using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairMoppingStageSuccessChecker : MonoBehaviour, ISuccessChecker
{
    [SerializeField] protected int _actionsLeftToDo;

    RepairStateSwitcher _repairStateSwitcher;
    [SerializeField] GameObject _fx, _mopper;
    [Header("Game Analytics")]
    [SerializeField] string _stageName;

    protected void Awake()
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
            _mopper.SetActive(false);
            _repairStateSwitcher.SwitchState(RepairStage.Reset, null);
            //SEND GA EVENT
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Repair_Scene", _stageName);
        }
    }
}
