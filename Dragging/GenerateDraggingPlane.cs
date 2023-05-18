using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDraggingPlane : MonoBehaviour
{
    [SerializeField] float _verticalTargetValue;
    [SerializeField] GameObject _anchor1, _anchor2;
    public GameObject GenerateDragPlane(Vector3 position, bool isAligned = true)
    {
        GameObject colliderObject = new GameObject();
        colliderObject.transform.position = position;
        colliderObject.transform.localScale = new Vector3(20f, 20f, 0.05f);

        colliderObject.AddComponent<BoxCollider>();
        colliderObject.GetComponent<BoxCollider>().isTrigger = true;
        colliderObject.layer = LayerMask.NameToLayer("DragPlane");
        if (isAligned)
            colliderObject.transform.rotation = Quaternion.FromToRotation(transform.forward, CalculateNormal(position)) * transform.rotation;
        return colliderObject;
    }

    private Vector3 CalculateNormal(Vector3 raycastOrigin)
    {
        Vector3 side1, side2;
        if (raycastOrigin.y > _verticalTargetValue)
        {
            Vector3 box1offset = _anchor1.transform.position;
            box1offset.y = _verticalTargetValue;
            Vector3 box3offset = _anchor2.transform.position;
            box3offset.y = _verticalTargetValue;
            side1 = box1offset - raycastOrigin;
            side2 = box3offset - raycastOrigin;
        }
        else
        {
            raycastOrigin.y = _verticalTargetValue;
            Vector3 box1offset = _anchor1.transform.position;
            box1offset.y = _verticalTargetValue;
            Vector3 box3offset = _anchor2.transform.position;
            box3offset.y = _verticalTargetValue;
            side1 = box1offset - raycastOrigin;
            side2 = box3offset - raycastOrigin;
        }

        return Vector3.Cross(side1, side2);
    }
}
