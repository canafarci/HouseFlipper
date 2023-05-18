using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageEndStageSuccessChecker : MonoBehaviour, ISuccessChecker
{
    [SerializeField] Material _uIMaterial;
    [SerializeField] Image _truckImage;
    [SerializeField] GameObject _greenBar, _directionArrow, _whiteDashLines, _greenDashLines;

    int _actionsNeededToDo = 3;

    public void OnActionSuccessful()
    {
        _actionsNeededToDo--;

        if (_actionsNeededToDo == 0)
        {
            _truckImage.material = _uIMaterial;
            _greenBar.SetActive(true);
            _directionArrow.SetActive(true);
            _whiteDashLines.SetActive(false);
            _greenDashLines.SetActive(true);

        }

    }
}
