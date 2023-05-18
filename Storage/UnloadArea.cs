using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadArea : MonoBehaviour
{
    [SerializeField] UnloadTransform[] _unloadTransforms;
    Mover _mover;
    StorageSuccessChecker _successChecker;

    private void Awake()
    {
        _mover = FindObjectOfType<Mover>();
        _successChecker = FindObjectOfType<StorageSuccessChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(UnloadItems(other.GetComponent<Stacker>().ItemStack));
    }

    IEnumerator UnloadItems(Stack<StackableItem> stack)
    {
        _mover.IsActive = false;
        while (stack.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            UnloadTransform unloadTarget = GetEmptyTransform();
            StackableItem item = stack.Pop();

            item.transform.parent = unloadTarget.transform.parent;
            Vector3 endPos = unloadTarget.transform.localPosition;
            Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y * 5f, endPos.z / 2f);
            Vector3[] path = { intermediatePos, endPos };

            item.transform.DOLocalPath(path, 0.5f, PathType.CatmullRom, PathMode.Full3D);
            item.transform.DOLocalRotate(unloadTarget.transform.eulerAngles, 0.5f);
            item.transform.DOScale(new Vector3(35f, 35f, 35f), 0.5f);
            StartCoroutine(DotweenFX(item, unloadTarget));
            _successChecker.OnDropItems();
        }
        _mover.IsActive = true;
    }

    IEnumerator DotweenFX(StackableItem item, UnloadTransform unloadTransform)
    {
        yield return new WaitForSeconds(0.5f);
        item.transform.DOPunchScale(new Vector3(10f, 20f, 10f), 0.3f);
        unloadTransform.PlayFX();
    }

    UnloadTransform GetEmptyTransform()
    {
        foreach (UnloadTransform ut in _unloadTransforms)
        {
            if (ut.IsFull == false)
            {
                ut.IsFull = true;
                return ut;
            }
        }
        return null;
    }
}
