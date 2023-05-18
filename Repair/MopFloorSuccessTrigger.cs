using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopFloorSuccessTrigger : MonoBehaviour
{
    bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasTriggered && other.CompareTag("Player"))
        {
            _hasTriggered = true;
            StartCoroutine(DelayedDisableCollider());
        }
    }

    IEnumerator DelayedDisableCollider()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponentInParent<ISuccessChecker>().OnActionSuccessful();
        GetComponent<Collider>().enabled = false;
    }
}
