using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stacker : MonoBehaviour
{
    public Stack<StackableItem> ItemStack { get { return _stack; } }
    Stack<StackableItem> _stack = new Stack<StackableItem>(); 
    StackPositionCalculator _positionCalculator;

    private void Awake()
    {
        _positionCalculator = GetComponent<StackPositionCalculator>();
    }

    public void StackItem(StackableItem item)
    {
        item.transform.parent = transform;
        Vector3 endPos = _positionCalculator.CalculatePosition(_stack, item);
        Vector3 intermediatePos = new Vector3(endPos.x /2f, endPos.y + 1f, endPos.z /2f);
        Vector3[] path = { intermediatePos, endPos};

        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);
        item.transform.DOLocalRotate(new Vector3(-90f, -180f, item.ItemTargetRotation),  0.5f);
        StartCoroutine(DotweenFX(item));
        _stack.Push(item);
    }

    IEnumerator DotweenFX(StackableItem item)
    {
        yield return new WaitForSeconds(0.5f);
        item.transform.DOPunchScale(new Vector3(10f, 20f, 10f), 0.3f);
    }
}
