using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPosition : MonoBehaviour
{
    public void SetCameraPosition(GameObject camera, float FOV)
    {
        camera.transform.position = transform.position;
        camera.transform.rotation = transform.rotation;
        camera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = FOV;
    }
}
