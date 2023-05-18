using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMopper : MonoBehaviour, IRaycastable
{
    [SerializeField] GameObject _mopper;
    public void OnBeginRaycast(Vector3 hitPoint)
    {
        _mopper.transform.position = Vector3.Lerp(_mopper.transform.position, hitPoint, Time.deltaTime * 5f);
    }
}
