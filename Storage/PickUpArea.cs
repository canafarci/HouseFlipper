using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpArea : MonoBehaviour
{
    [SerializeField] StackableItem[] _pickUps;
    Mover _mover;

    private void Awake()
    {
        _mover = FindObjectOfType<Mover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadItemsToLift(other.GetComponent<Stacker>()));
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator LoadItemsToLift(Stacker stacker)
    {
        _mover.IsActive = false;
        foreach (StackableItem si in _pickUps)
        {
            stacker.StackItem(si);
            yield return new WaitForSeconds(1f);
        }
        _mover.IsActive = true;
        GetComponent<ICustomStage>().Activate();
    }
}
