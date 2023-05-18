using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastableShootButton : MonoBehaviour, IRaycastable
{
    [SerializeField] GameObject _fx, _fx2, _button, _hammerAnimation;
    [SerializeField] RayfireGun _gun;
    [SerializeField] Transform _target;
    [SerializeField] bool _lastButton;
    public void OnBeginRaycast(Vector3 hitPoint)
    {
        GameObject hammer = Instantiate(_hammerAnimation, transform.position, _hammerAnimation.transform.rotation);
        Destroy(hammer, 1f);
        StartCoroutine(OnButtonClick());
        transform.GetChild(0).transform.gameObject.SetActive(false);
    }

    IEnumerator OnButtonClick()
    {
        yield return new WaitForSeconds(0.5f);
        if (!_lastButton)
            GetComponentInParent<ISuccessChecker>().OnActionSuccessful();

        _gun.target = _target;
        _gun.Shoot();
        _fx.SetActive(true);
        _fx2.SetActive(true);
        if (_button != null)
            _button.SetActive(true);
        gameObject.SetActive(false);
    }


}
