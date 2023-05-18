using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationManager : MonoBehaviour
{
    public float GetAnimationLength(string name, Animator animator)
    {
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            if (animator.runtimeAnimatorController.animationClips[i].name == name)
                return animator.runtimeAnimatorController.animationClips[i].length;
        }

        Debug.LogError("Animation clip: " + name + " not found");
        return 0;
    }
}
