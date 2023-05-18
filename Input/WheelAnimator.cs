using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimator : MonoBehaviour
{
    [SerializeField] Transform[] _wheelTransforms;
    InputReader _reader;

    private void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
    }

    private void FixedUpdate()
    {
        Rotate(_reader.ReadInput());
    }

    private void Rotate(Vector2 moveVector)
    {
        foreach (Transform tr in _wheelTransforms)
        {
            Vector3 rotation = tr.rotation.eulerAngles;
            rotation.z -= Mathf.Abs(moveVector.y + moveVector.x) * Time.fixedDeltaTime * 500f;
            tr.eulerAngles = rotation;
        }
    }
}
