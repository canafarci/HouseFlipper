using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRaycaster : MonoBehaviour
{
    [SerializeField] LayerMask _startLayer;
    bool _isSwiping = false;

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
        if (state != RaycastState.Swipe)
            _isSwiping = false;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
            _isSwiping = true;
        else if (Input.GetMouseButtonUp(0))
            _isSwiping = false;

        if (_isSwiping)
            RaycastForSwipe();
    }

    void RaycastForSwipe()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _startLayer))
        {
            hit.transform.GetComponent<IRaycastable>().OnBeginRaycast(hit.point);
        }
    }
}
