using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDragAndDropTarget : MonoBehaviour
{
    [HideInInspector] public Image StartDragImage;

    [SerializeField] GameObject _uIFX, _fX2;
    UIDragAndDropStageSuccessChecker _controller;
    Renderer[] _renderers;

    Color _defaultColor = new Color(0, 1, 0, 120f / 255f);
    Color _greenColor = new Color(1, 1, 1, 120f / 255f);

    private void Awake()
    {
        _controller = GetComponentInParent<UIDragAndDropStageSuccessChecker>();
        _renderers = GetComponentsInChildren<Renderer>(true);
    }

    private void Start() => _controller.DragsLeft++;

    public void ColorLerp(float distance, float maxDistance)
    {
        foreach (Renderer renderer in _renderers)
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                Material objectMaterial = renderer.materials[i];
                objectMaterial.color = Color.Lerp(_defaultColor, _greenColor, distance / maxDistance);
                renderer.materials[i] = objectMaterial;
            }
        }
    }

    public void OnDragSuccessful()
    {
        _uIFX.SetActive(true);
        _fX2.SetActive(true);
        _controller.OnDragSuccessful();

        StartDragImage.raycastTarget = false;

        ICustomStage customStage = GetComponent<ICustomStage>();
        if (customStage != null)
            customStage.Activate();
    }
}
