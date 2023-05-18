using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepairStageWithColliderSuccessChecker : MonoBehaviour, ISuccessChecker
{

    int _actionsLeftToDo;
    RepairStateSwitcher _repairStateSwitcher;
    [SerializeField] GameObject _fx;
    [Header("Game Analytics")]
    [SerializeField] string _stageName;

    private void Awake()
    {
        _repairStateSwitcher = FindObjectOfType<RepairStateSwitcher>();

        foreach (Collider col in GetComponentsInChildren<Collider>(true).Where(x => x.gameObject.transform != transform))
        {
            _actionsLeftToDo++;
        }
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
            //SEND GA EVENT
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Repair_Scene", _stageName);
        }
    }
}
