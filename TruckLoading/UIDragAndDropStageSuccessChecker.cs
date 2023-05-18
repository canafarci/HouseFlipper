using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EasyMobile;

public class UIDragAndDropStageSuccessChecker : MonoBehaviour
{
    public int DragsLeft;
    [SerializeField] bool _tweenEndStage, _showAd;
    [SerializeField] GameObject _fx, _nextStage, _nextStageCanvas, _oldCanvas;
    [SerializeField] int _sceneIndexToLoad;

    public void OnDragSuccessful()
    {
        DragsLeft--;
        if (DragsLeft == 0)
        {
            _fx.SetActive(true);
            if (_tweenEndStage)
                transform.DOPunchScale(new Vector3(.3f, .6f, .3f), 0.8f);

            if (_nextStage != null)
            {
                StartCoroutine(ManageActivation());

                if (_showAd)
                {
                    if (Advertising.IsInterstitialAdReady())
                        Advertising.ShowInterstitialAd();
                }
            }
            else
                EndGame();
        }
            

        
    }

    IEnumerator ManageActivation()
    {
        yield return new WaitForSeconds(2f);
        _nextStageCanvas.SetActive(true);
        _nextStage.SetActive(true);
        _oldCanvas.SetActive(false);
        gameObject.SetActive(false);
    }

    private void EndGame()
    {
        if (Advertising.IsInterstitialAdReady())
            Advertising.ShowInterstitialAd();

        GameManager.Instance.SceneLoader.FadedLoadScene(_sceneIndexToLoad, 2f);
    }
}
