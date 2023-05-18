using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColliderStageStarter : MonoBehaviour, IStageStarter
{
    List<Collider> _colliders = new List<Collider>();

    private void Awake()
    {
        foreach (Collider c in GetComponentsInChildren<Collider>(true).Where(x => x.gameObject.transform != transform))
        {
            _colliders.Add(c);
        }
    }

    public void StartStage()
    {
        foreach (Collider c in _colliders)
        {
            c.enabled = true;
        }
    }
}
