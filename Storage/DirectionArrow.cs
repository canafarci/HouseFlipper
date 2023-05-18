using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    public Transform Target;

    private void LateUpdate()
    {
        var lookPos = Target.position - transform.position;
        lookPos.y = transform.position.y;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
}
