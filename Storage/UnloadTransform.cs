using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadTransform : MonoBehaviour
{
    public bool IsFull = false;
    [SerializeField] GameObject _fx;

    public void PlayFX()
    {
        _fx.SetActive(true);
    }

}
