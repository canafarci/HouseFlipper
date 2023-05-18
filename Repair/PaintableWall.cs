using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PaintableWall : MonoBehaviour, IRaycastable
{
    Renderer _renderer;
    [SerializeField] GameObject[] _fxs;
    GameObject _fxToPlay;

    Color _color = new Color(37f/255f, 153f / 255f, 205f / 255f);
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        List<float> distList = new List<float>();

        foreach (GameObject go in _fxs)
        {
            distList.Add(Vector3.Distance(transform.position, go.transform.position));
        }

        float lowest_dist = distList.Min();
        int index = distList.IndexOf(lowest_dist);
        _fxToPlay = _fxs[index];
        distList.Clear();
    }

    public void OnBeginRaycast(Vector3 hitPoint)
    {
        GetComponentInParent<ISuccessChecker>().OnActionSuccessful();
        GetComponent<Collider>().enabled = false;

        foreach (Material mat in _renderer.materials)
        {
            mat.DOColor(_color, "Color_1", 1);
            mat.DOColor(_color, "Color_48988742e4794357977a336c68947f59", 1);
        }

        PlayFX(hitPoint);
        this.enabled = false;
    }

    private void PlayFX(Vector3 hitPosition)
    {
        GameObject fxPrefab = GameManager.Instance.References.GameConfig.WallFX;
        GameObject fx = Instantiate(fxPrefab, hitPosition, fxPrefab.transform.rotation);
        Destroy(fx, 3f);

        AnimationSetInactive animationTrigger =  GetComponentInChildren<AnimationSetInactive>(true);
        animationTrigger.gameObject.SetActive(true);

        _fxToPlay.SetActive(true);
    }
}
