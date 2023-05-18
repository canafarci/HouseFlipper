using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetInactive : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
