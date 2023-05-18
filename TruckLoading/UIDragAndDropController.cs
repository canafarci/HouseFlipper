using EasyMobile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragAndDropController : MonoBehaviour
{
    public int DragsLeft;

    [SerializeField] GameObject _fx;
    [SerializeField] int _sceneIndexToLoad;

    public void OnDragSuccessful()
    {
        DragsLeft--;
        if (DragsLeft == 0)
            EndGame();
    }

    private void EndGame()
    {
        _fx.SetActive(true);

        if (Advertising.IsInterstitialAdReady())
            Advertising.ShowInterstitialAd();

        GameManager.Instance.SceneLoader.FadedLoadScene(_sceneIndexToLoad, 2f);
    }
}
