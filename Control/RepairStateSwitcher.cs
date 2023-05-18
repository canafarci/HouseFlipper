using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStateSwitcher : MonoBehaviour
{
    LevelCompleteChecker _levelCompleteChecker;
    RaycastStateMachine _stateMachine;
    public RepairStage ActiveStage;

    public event Action OnStageStart, OnStageEnd;
    public event Action<RaycastState> OnRaycastStateChange;

    private void Awake()
    {
        _stateMachine = FindObjectOfType<RaycastStateMachine>();
        _levelCompleteChecker = FindObjectOfType<LevelCompleteChecker>();
    }

    public void SwitchState(RepairStage stage, IStageStarter stageStarter)
    {
        ActiveStage = stage;
        StartCoroutine(SwitchStage(stage));

        if (stageStarter != null)
            stageStarter.StartStage();

        if (stage == RepairStage.Reset)
        {
            StageOver();
            OnStageEnd?.Invoke();
        }
        else
        {
            OnStageStart?.Invoke();
        }
    }

    IEnumerator SwitchStage(RepairStage stage)
    {
        yield return new WaitForSeconds(1f);
        switch (stage)
        {
            case RepairStage.Wall:
                _stateMachine.ActiveState = RaycastState.Swipe;
                OnRaycastStateChange(RaycastState.Swipe);
                break;
            case RepairStage.Floor:
                _stateMachine.ActiveState = RaycastState.Swipe;
                OnRaycastStateChange(RaycastState.Swipe);
                break;
            case RepairStage.Window:
                break;
            case RepairStage.Paint:
                _stateMachine.ActiveState = RaycastState.Swipe;
                OnRaycastStateChange(RaycastState.Swipe);
                break;
            case RepairStage.Mopping:
                _stateMachine.ActiveState = RaycastState.Swipe;
                OnRaycastStateChange(RaycastState.Swipe);
                break;
            default:
                break;
        }
    }

    void StageOver()
    {
        GameManager.Instance.CameraController.ActivateCamera(CameraStrings.FirstCamera);
        _stateMachine.ActiveState = RaycastState.Click;
        OnRaycastStateChange(RaycastState.Click);
        _levelCompleteChecker.OnStageComplete();
    }
}
