using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectAfterSuccess : MonoBehaviour, ICustomStage
{
    [SerializeField] GameObject[] _objectsToActivate;
    [SerializeField] GameObject[] _objectsToDeactivate;

    public void Activate()
    {
        if (_objectsToActivate.Length > 0)
            foreach (GameObject go in _objectsToActivate)
            {
                go.SetActive(true);
            }

        if (_objectsToDeactivate.Length > 0)
            foreach (GameObject go in _objectsToDeactivate)
            {
                go.SetActive(false);
            }

    }
}
