using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisDraggable : MonoBehaviour, IDraggable
{
    [HideInInspector] public bool IsInWrongPlace = false;

    UIDragAndDropController _controller;
    GameObject _checkMark;
    [SerializeField] GameObject _correctPositionMark, _wrongPositionMark;
    Image _raycastImage;
    
    private void Awake() => _controller = FindObjectOfType<UIDragAndDropController>();

    public void SetReferences(Image image, GameObject checkMark)
    {
        _checkMark = checkMark;
        _raycastImage = image;
    }

    public void OnBeginRaycast(Vector3 hitPoint)
    {
        //foreach (TetrisDraggable td in FindObjectsOfType<TetrisDraggable>())
        //{
        //    if(td.IsInWrongPlace && td != this)
        //        Destroy(td.gameObject);
        //}

        Destroy(gameObject);
    }

    public void OnDragFail()
    {
        IsInWrongPlace = true;
        _wrongPositionMark.SetActive(true);
    }

    public void OnDragEnd(Vector3 position)
    {
        transform.DOPunchScale(new Vector3(0.2f, 0.5f, 0.2f), 1f, 10);
        _controller.OnDragSuccessful();

        foreach (Collider col in GetComponents<Collider>())
        {
            col.isTrigger = false;
        }
        gameObject.layer = LayerMask.NameToLayer("Default");

        _checkMark.SetActive(true);
        _raycastImage.raycastTarget = false;
        _correctPositionMark.SetActive(true);
    }
}
