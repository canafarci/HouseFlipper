using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] GameObject _dragObject;
    [SerializeField] UIDragAndDropTarget _target;
    CanvasDragRaycaster _loadingRaycaster;

    private void Awake()
    {
        _loadingRaycaster = FindObjectOfType<CanvasDragRaycaster>();
        _target.StartDragImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("called");
        _loadingRaycaster.StartRaycast(_target.transform, _dragObject);
        _target.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //needs to be implemented to use OnBeginDrag
    }
}
