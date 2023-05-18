using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragAndDropActivateAfterSuccess : MonoBehaviour, ICustomStage
{
    [SerializeField] GameObject _objectToActivate;
    public void Activate()
    {
        _objectToActivate.SetActive(true);
    }
}
