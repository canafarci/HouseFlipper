using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragAndDropDraggable : MonoBehaviour, IDraggable
{
    void Tween()
    {
        transform.DOPunchScale(new Vector3(10f, 20f, 10f), 0.3f);
    }

    public void OnDragEnd(Vector3 position)
    {
        Tween();
    }

    public void OnDragFail()
    {
    }

    public void OnBeginRaycast(Vector3 hitPoint)
    {
    }
}
