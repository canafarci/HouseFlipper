using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastStateMachine : MonoBehaviour
{
    public RaycastState ActiveState;

    ClickRaycaster _clickRaycaster;
    DragRaycaster _dragRaycaster;
    SwipeRaycaster _swipeRaycaster;

    private void Awake()
    {
        _clickRaycaster = GetComponent<ClickRaycaster>();
        _dragRaycaster = GetComponent<DragRaycaster>();
        _swipeRaycaster = GetComponent<SwipeRaycaster>();
    }

    private void Update()
    {
        switch (ActiveState)
        {
            case RaycastState.Inactive:
                return;
            case RaycastState.Click:
                _clickRaycaster.Tick();
                break;
            case RaycastState.Drag:
                _dragRaycaster.Tick();
                break;
            case RaycastState.Swipe:
                _swipeRaycaster.Tick();
                break;
            default:
                break;
        }
    }
}
