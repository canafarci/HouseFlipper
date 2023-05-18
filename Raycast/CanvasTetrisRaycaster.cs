using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTetrisRaycaster : MonoBehaviour
{
    [SerializeField] GameObject _draggingPlane;
    [SerializeField] LayerMask _dragLayer, _dropLayer, _startLayer;
    [SerializeField] float _lerpSpeed;

    bool _isDragging;
    GameObject _dragObject;
    TetrisDragAndDropTarget _dropTarget;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            RaycastForReturn(ray);

        if (!_isDragging) { return; }

        DraggingBehaviour(ray);

        if (Input.GetMouseButtonUp(0))
            EndRaycast(ray);
    }

    void RaycastForReturn(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _startLayer))
        {
            hit.transform.GetComponent<IRaycastable>().OnBeginRaycast(hit.point);
        }
    }

    public void StartRaycast(GameObject objectToDrag, TetrisDragAndDropTarget target)
    {
        _isDragging = true;
        _dragObject = objectToDrag;
        _dropTarget = target;
    }

    private void DraggingBehaviour(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _dragLayer))
        {
            _dragObject.transform.position = Vector3.Lerp(_dragObject.transform.position, hit.point, Time.deltaTime * _lerpSpeed);
        }
    }

    private void EndRaycast(Ray ray)
    {
        TetrisCollisionChecker collisionChecker = _dragObject.GetComponent<TetrisCollisionChecker>();

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _dropLayer))
        {
            _dragObject.transform.position = hit.transform.position;
            StartCoroutine(collisionChecker.DelayedCheckCollision());

            if (_dropTarget.gameObject == hit.transform.gameObject)
                _dragObject.transform.GetComponent<IDraggable>().OnDragEnd(hit.point);
            else
                _dragObject.transform.GetComponent<IDraggable>().OnDragFail();
        }
        else
        {
            print("failed");
            Destroy(_dragObject);
        }

        _dragObject = null;
        _dropTarget = null;
        _isDragging = false;
    }
}
