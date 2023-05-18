using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRepairStageRaycastable : MonoBehaviour, IRaycastable, IStageStateCallback
{
    public bool PastStage3 = false;
    CameraPosition _cameraPosition;
    RepairStateSwitcher _stageSwitcher;
    [SerializeField] RepairStage _stageToSwitch;
    [SerializeField] float _fovForStage;
    [SerializeField] GameObject[] _stageButtons;
    Collider _collider;
    [Header("Game Analytics")]
    [SerializeField] string _stageName;

    private void Awake()
    {
        _cameraPosition = GetComponentInChildren<CameraPosition>();
        _stageSwitcher = FindObjectOfType<RepairStateSwitcher>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _stageSwitcher.OnStageStart += StateStartCallback;
        _stageSwitcher.OnStageEnd += StageEndCallback;
    }

    private void OnDisable()
    {
        _stageSwitcher.OnStageStart -= StateStartCallback;
        _stageSwitcher.OnStageEnd -= StageEndCallback;
    }

    public void OnBeginRaycast(Vector3 hitPoint)
    {
        if (!PastStage3)
            return;

        GameObject camObject = GameManager.Instance.CameraController.ActivateCamera(CameraStrings.SecondCamera);
        _cameraPosition.SetCameraPosition(camObject, _fovForStage);
        _stageSwitcher.SwitchState(_stageToSwitch, GetComponent<IStageStarter>());
        //SEND GA EVENT
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Repair_Scene", _stageName);
        this.enabled = false;
    }

    public void StateStartCallback()
    {
        _collider.enabled = false;
        ChangeActivationOfObjects(false);
    }

    public void StageEndCallback()
    {
        if (PastStage3)
        {
            _collider.enabled = true;
            ChangeActivationOfObjects(true);
        }
    }

    void ChangeActivationOfObjects(bool active)
    {
        if (_stageButtons != null)
            foreach (GameObject go in _stageButtons)
            {
                go.SetActive(active);
            }
    }
}
