using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable : IRaycastable
{
    void OnDragEnd(Vector3 position);
    void OnDragFail();
}
