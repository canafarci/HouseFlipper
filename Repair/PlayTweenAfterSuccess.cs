using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayTweenAfterSuccess : MonoBehaviour, ICustomStage
{
    [SerializeField] GameObject[] _floorParts;
    [SerializeField] GameObject _finalObjectToActivate, _finalObjectToDeactivate;
    [SerializeField] float _duration;
    public void Activate()
    {
        StartCoroutine(PlayTweenAnimation());
    }

    IEnumerator PlayTweenAnimation()
    {
        GameObject fxPrefab = GameManager.Instance.References.GameConfig.FloorFX;
        

        for (int i = 0; i < _floorParts.Length; i++)
        {
            _floorParts[i].SetActive(true);
            _floorParts[i].transform.DOMove(new Vector3(1.2f, 1.2f, 1.2f), _duration).From();
            yield return new WaitForSeconds(_duration);
            _floorParts[i].transform.DOPunchScale(new Vector3(10f, 20f, 10f), _duration);

            Mesh mesh = _floorParts[i].GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            Vector3 vertexWorldPosition1 = _floorParts[i].transform.TransformPoint(vertices[0]);
            Vector3 vertexWorldPosition2 = _floorParts[i].transform.TransformPoint(vertices[2]);

            GameObject fx = Instantiate(fxPrefab, (vertexWorldPosition2 + vertexWorldPosition1) / 2f, fxPrefab.transform.rotation);
            Destroy(fx, 3f);

            yield return new WaitForSeconds(_duration);
        }

        _finalObjectToActivate.SetActive(true);
        _finalObjectToDeactivate.SetActive(false);
    }
}
