using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivator : MonoBehaviour, ICustomStage
{
    [SerializeField] GameObject _objectToActivate;
    StorageEndStageSuccessChecker _uIEndStage;

    private void Awake()
    {
        _uIEndStage = FindObjectOfType<StorageEndStageSuccessChecker>();
    }
    public void Activate()
    {
        _objectToActivate.SetActive(true);
        _uIEndStage.OnActionSuccessful();
        gameObject.SetActive(false);
    }
}
