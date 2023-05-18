using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRaycaster : MonoBehaviour
{
    [SerializeField] LayerMask _startLayer, _dragLayer, _endLayer;
    [SerializeField] float _lerpSpeed;

    bool _isDragging;
    GameObject _draggedObject, _colliderObject;
    Vector3 _startPosition;

    GenerateDraggingPlane _planeGenerator;

    private void Awake()
    {
        _planeGenerator = GetComponent<GenerateDraggingPlane>();
    }

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            StartDragging(ray);

        if (_isDragging)
            DraggingBehaviour(ray);

        if (Input.GetMouseButtonUp(0))
            EndRaycast(ray);
    }

    void StartDragging(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _startLayer))
        {
            _draggedObject = hit.transform.gameObject;
            _startPosition = _draggedObject.transform.position;
            _isDragging = true;
            _colliderObject = _planeGenerator.GenerateDragPlane(hit.point);

            hit.transform.GetComponent<IRaycastable>().OnBeginRaycast(hit.point);
        }
    }

    void DraggingBehaviour(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _dragLayer))
        {
            _draggedObject.transform.position = Vector3.Lerp(_draggedObject.transform.position, hit.point, Time.deltaTime * _lerpSpeed);
        }
    }

    private void EndRaycast(Ray ray)
    {
        if (_draggedObject == null) { return; }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _endLayer))
        {
            hit.transform.gameObject.GetComponent<IDropTarget>().OnDrop();
            _draggedObject.gameObject.GetComponent<IDraggable>().OnDragEnd(hit.transform.position);
        }
        else
        {
            if (_draggedObject != null)
                _draggedObject.transform.position = _startPosition;
        }

        _draggedObject = null;
        _isDragging = false;
        Destroy(_colliderObject);
        _colliderObject = null;
    }
}
