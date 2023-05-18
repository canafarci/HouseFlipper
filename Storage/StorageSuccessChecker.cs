using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSuccessChecker : MonoBehaviour
{
    [SerializeField] int _actionsNeededToDo, _sceneIndexToLoad;

    public void OnDropItems()
    {
        _actionsNeededToDo--;

        if (_actionsNeededToDo == 0)
        {
            if (Advertising.IsInterstitialAdReady())
                Advertising.ShowInterstitialAd();

            print("Level Over");
            GameManager.Instance.SceneLoader.FadedLoadScene(_sceneIndexToLoad, 1.5f);
        }
    }
}
