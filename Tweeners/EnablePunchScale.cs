using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnablePunchScale : MonoBehaviour
{
    [SerializeField] Transform _frame;

    private void OnEnable()
    {
        StartCoroutine(DotweenFX());
    }

    IEnumerator DotweenFX()
    {
        transform.DOScale(new Vector3(0.04747932f, 1f, 1f), 0.75f);
        yield return new WaitForSeconds(0.75f);
        _frame.parent = transform;
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1f);
        StartCoroutine(DelayedLastButton());
    }

    IEnumerator DelayedLastButton()
    {
        yield return new WaitForSeconds(1f);
        GetComponentInParent<ISuccessChecker>().OnActionSuccessful();
    }
}
