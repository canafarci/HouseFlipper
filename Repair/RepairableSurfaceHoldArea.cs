using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableSurfaceHoldArea : MonoBehaviour, IRaycastable
{
    [SerializeField] float _duration;
    [SerializeField] GameObject _fx;

    [SerializeField]  SkinnedMeshRenderer _skinnedMeshRenderer;
    Collider _collider;
    float _cooldown = 0f;
    Coroutine _blendCoroutine;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_cooldown > 0f)
            _cooldown -= Time.deltaTime;
    }

    public void OnBeginRaycast(Vector3 hitPoint)
    {
        if (_cooldown > 0f) { return; }

        _fx.SetActive(true);

        _cooldown += _duration;

        if (_blendCoroutine != null)
            StopCoroutine(_blendCoroutine);

        _blendCoroutine = StartCoroutine(BlendShape(_duration, hitPoint));
    }

    IEnumerator BlendShape(float duration, Vector3 hitPoint)
    {
        //2secs per stage

        //transform.DOPunchScale(new Vector3(10f, 10f, 15f), .3f);

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            _fx.transform.position = Vector3.Lerp(_fx.transform.position, hitPoint, Time.deltaTime * 5f);

            float blendPercentage = (_skinnedMeshRenderer.GetBlendShapeWeight(0));
            blendPercentage += (i * 1000f * Time.deltaTime);
            _skinnedMeshRenderer.SetBlendShapeWeight(0, blendPercentage);
            yield return new WaitForSeconds(Time.deltaTime);
            if (_skinnedMeshRenderer.GetBlendShapeWeight(0) > 100f)
            {
                GetComponentInParent<ISuccessChecker>().OnActionSuccessful();
                _collider.enabled = false;
                _fx.SetActive(false);
                break;
            }
        }

        _fx.SetActive(false);


    }

    
}
