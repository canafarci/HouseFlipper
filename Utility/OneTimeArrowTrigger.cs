using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeArrowTrigger : MonoBehaviour
{
    [SerializeField] string _tagName;
    [SerializeField] Transform _finalTarget;
    DirectionArrow _arrow;
    bool _actionFinished = false;

    private void Awake()
    {
        _arrow = FindObjectOfType<DirectionArrow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagName) && !_actionFinished)
        {
            _arrow.Target = _finalTarget;
            _arrow.gameObject.SetActive(false);
            _actionFinished = true;
        }
    }


}
