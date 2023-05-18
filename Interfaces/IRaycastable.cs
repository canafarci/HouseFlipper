using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycastable
{
    void OnBeginRaycast(Vector3 hitPoint);
}
