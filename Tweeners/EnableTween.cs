using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnableTween : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOPunchScale(new Vector3(0.4f, .4f, .4f), 1f);
    }
}
