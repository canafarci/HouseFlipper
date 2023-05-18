using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class DragCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cam;
    [SerializeField] float _speed;
    CinemachineTrackedDolly _dolly;
    bool _isDragging;
    float _oldX;

    private void Awake()
    {
        _dolly = _cam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            _oldX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            _isDragging = true;
        }
            
        else if (Input.GetMouseButtonUp(0))
            _isDragging = false;
    }


    private void FixedUpdate()
    {
        if (_isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float currentX = mousePos.x;

            //swipe right
            if (currentX > _oldX && _dolly.m_PathPosition < 1f)
            {
                //cinemachine dolly right
                _dolly.m_PathPosition += (currentX - _oldX) * Time.fixedDeltaTime * _speed;

            }
            else if ( currentX < _oldX && _dolly.m_PathPosition > 0f)
            {
                //cinemachine dolly left
                _dolly.m_PathPosition += (currentX - _oldX) * Time.fixedDeltaTime * _speed;
            }
        }

        
    }
}
