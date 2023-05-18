using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDragRaycaster : MonoBehaviour
{
    [SerializeField] GameObject _draggingPlane;
    [SerializeField] LayerMask _dragLayer, _dropLayer;
    [SerializeField] float _lerpSpeed;
    [SerializeField] bool _isAssemblyScene = false;


    bool _isDragging;
    GameObject _dragObject;
    Transform _targetTransform;


     void Update()
    {
        if (!_isDragging) { return; }

        DraggingBehaviour();

        if (Input.GetMouseButtonUp(0))
            EndRaycast();
    }
    public void StartRaycast(Transform targetTransform, GameObject objectToDrag)
    {
        _targetTransform = targetTransform;
        AlignDraggingPlane(targetTransform.position);
        _isDragging = true;
        _dragObject = Instantiate(objectToDrag, objectToDrag.transform.position, objectToDrag.transform.rotation);
    }

    private void DraggingBehaviour()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _dragLayer))
        {
            _dragObject.transform.position = Vector3.Lerp(_dragObject.transform.position, hit.point, Time.deltaTime * _lerpSpeed);
            if (_isAssemblyScene && _dragObject.transform.position.z < _targetTransform.position.z)
            {
                Vector3 pos = _dragObject.transform.position;
                pos.z = _targetTransform.position.z;
                _dragObject.transform.position = pos;
            }
                

            float distance = Vector3.Distance(_dragObject.transform.position, _targetTransform.position);
            UIDragAndDropTarget target = _targetTransform.GetComponent<UIDragAndDropTarget>();

            if (distance < 5f)
            {
                target.ColorLerp(distance, 5f);
            }
        }
    }

    private void EndRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _dropLayer) && hit.transform == _targetTransform)
        {
            _dragObject.transform.position = _targetTransform.position;
            _dragObject.transform.parent = _targetTransform.parent;
            _dragObject.transform.GetComponent<IDraggable>().OnDragEnd(hit.point);
            UIDragAndDropTarget target = _targetTransform.GetComponent<UIDragAndDropTarget>();
            target.OnDragSuccessful();
            _targetTransform.GetChild(0).GetComponent<Renderer>().enabled = false;
            _targetTransform.GetComponent<Collider>().enabled = false ;

        }
        else
        {
            print("failed");
            Destroy(_dragObject);
            _targetTransform.GetChild(0).gameObject.SetActive(false);

        }

        _dragObject = null;
        _isDragging = false;
        _targetTransform = null;
    }


    private void AlignDraggingPlane(Vector3 targetPosition)
    {
        _draggingPlane.transform.position = targetPosition + new Vector3(0f, 0.25f, 0f);
    }
}
