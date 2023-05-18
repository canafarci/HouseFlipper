using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHouseRaycastable : MonoBehaviour, IRaycastable
{
    public void OnBeginRaycast(Vector3 hitPoint)
    {
        GameManager.Instance.SceneLoader.FadedLoadScene(1, 2f);
    }
}
