using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStageWithBlendShapeSuccessChecker : MonoBehaviour, ISuccessChecker
{
    [SerializeField] GameObject _fx;
    int _actionsLeftToDo;

    RepairStateSwitcher _repairStateSwitcher;
    SkinnedMeshRenderer _skinnedMeshRenderer;
    [Header("Game Analytics")]
    [SerializeField] string _stageName;

    private void Awake()
    {
        _repairStateSwitcher = FindObjectOfType<RepairStateSwitcher>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        Mesh mesh = _skinnedMeshRenderer.sharedMesh;
        _actionsLeftToDo = mesh.blendShapeCount;
    }

    public void OnActionSuccessful()
    {
        _actionsLeftToDo--;

        if (_actionsLeftToDo == 0)
        {
            _fx.SetActive(true);
            ICustomStage[] customStages = GetComponents<ICustomStage>();
            if (customStages != null)
                foreach (ICustomStage cs in customStages)
                {
                    cs.Activate();
                }
                

            _repairStateSwitcher.SwitchState(RepairStage.Reset, null);

            //SEND GA EVENT
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Repair_Scene", _stageName);
        }
    }
}
