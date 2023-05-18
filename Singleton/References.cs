using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    public GameConfig GameConfig;
    public CanvasGroup CanvasGroup;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PrefKeys.Money))
            GameManager.Instance.Saver.SaveInt(PrefKeys.Money, GameManager.Instance.References.GameConfig.StartMoney);

        CanvasGroup = GetComponentInChildren<CanvasGroup>();
    }
}
