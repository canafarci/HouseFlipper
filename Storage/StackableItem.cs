using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : MonoBehaviour
{
    public float ItemHeight;
    [Range(0f, 360f)]
    public float ItemTargetRotation;
    Collider _collider;

    private void Awake() => _collider = GetComponent<Collider>();

    //private void OnTriggerEnter(Collider other)
    //{
    //    if ( other.CompareTag("Player"))
    //    {
    //        other.GetComponent<Stacker>().StackItem(this);
    //        _collider.enabled = false;
    //    }
    //}
}
