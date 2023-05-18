using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveStageStarter : MonoBehaviour, IStageStarter
{
    [SerializeField] GameObject _objectToActivate;
    [SerializeField] bool _delayedActivate;
    [SerializeField] float _delay;
    public void StartStage()
    {
        if (!_delayedActivate)
            _objectToActivate.SetActive(true);
        else
            StartCoroutine(DelayedActivate());
    }

    IEnumerator DelayedActivate()
    {
        yield return new WaitForSeconds(_delay);
        _objectToActivate.SetActive(true);
    }
}
