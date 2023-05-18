using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyMobile;

public class BiddingLooper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _priceText, _bidsLeftText;
    [SerializeField] int[] _bidPrices;
    [SerializeField] GameObject _fX;
    [SerializeField] int _sceneIndexToLoad;
    [SerializeField] GameObject[] _objectsToDisable, _objectsToEnable;

    int _lastBidIndex = 0;

    private void Awake()
    {
        _bidsLeftText.text = $"{_bidPrices.Length} OFFERS REMANING";
        _priceText.text = $"$ {_bidPrices[0]}";
    }

    public void OnAcceptButtonClicked()
    {
        _fX.SetActive(true);
        foreach (GameObject go in _objectsToDisable)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in _objectsToEnable)
        {
            go.SetActive(true);
        }

        if (Advertising.IsInterstitialAdReady())
            Advertising.ShowInterstitialAd();


        GameManager.Instance.SceneLoader.FadedLoadScene(_sceneIndexToLoad, 3f);
    }

    public void OnDeclineButtonClicked()
    {
        _lastBidIndex++;
        if (_lastBidIndex < _bidPrices.Length)
        {
            _priceText.text = $"$ {_bidPrices[_lastBidIndex]}";
            _bidsLeftText.text = $"{_bidPrices.Length - _lastBidIndex} OFFERS REMANING";
        }
            
        else
            OnAcceptButtonClicked();

    }
}
