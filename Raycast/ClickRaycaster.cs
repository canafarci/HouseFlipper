using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRaycaster : MonoBehaviour
{
    [SerializeField] LayerMask _startLayer;

    private void OnEnable()
    {
        FindObjectOfType<RepairStateSwitcher>().OnRaycastStateChange += OnRaycastStateChange;
    }

    private void OnDisable()
    {
        RepairStateSwitcher repairStateSwitcher = FindObjectOfType<RepairStateSwitcher>();

        if (repairStateSwitcher != null)
            repairStateSwitcher.OnRaycastStateChange -= OnRaycastStateChange;
    }

    private void OnRaycastStateChange(RaycastState state)
    {
        
    }


    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
            RaycastForClick();
    }

    void RaycastForClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _startLayer))
        {
            hit.transform.GetComponent<IRaycastable>().OnBeginRaycast(hit.point);
            IRaycastFX raycastFX = hit.transform.GetComponent<IRaycastFX>();
            if (raycastFX != null)
                raycastFX.PlayFX(hit.point);
        }
    }
}
