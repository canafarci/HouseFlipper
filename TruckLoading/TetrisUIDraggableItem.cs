using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TetrisUIDraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] GameObject _dragObject, _checkMark;
    [SerializeField] TetrisDragAndDropTarget _dropTarget;
    CanvasTetrisRaycaster _tetrisRaycaster;

    private void Awake()
    {
        _tetrisRaycaster = FindObjectOfType<CanvasTetrisRaycaster>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("called");
        GameObject objectToDrag =  Instantiate(_dragObject, _dragObject.transform.position, _dragObject.transform.rotation);
        objectToDrag.GetComponent<TetrisDraggable>().SetReferences(GetComponent<Image>(), _checkMark);
        _tetrisRaycaster.StartRaycast(objectToDrag, _dropTarget);

        foreach (TetrisDraggable td in FindObjectsOfType<TetrisDraggable>())
        {
            if (td.IsInWrongPlace && td != this)
                Destroy(td.gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //needs to be implemented to use OnBeginDrag
    }
}
