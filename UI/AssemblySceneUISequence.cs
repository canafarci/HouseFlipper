using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssemblySceneUISequence : MonoBehaviour
{
    [SerializeField] float _initialDelay = 0f;
    [SerializeField] RectTransform _tetrisImage, _itemImage, _grayBackGround;
    [SerializeField] GameObject _grayProgressBar;
    [SerializeField] RectTransform[] _draggableImages;
    [SerializeField] Vector3[] _draggableTargetPositions;
    

    void OnEnable()
    {
        StartCoroutine(UISequence());
    }

    IEnumerator UISequence()
    {
        _grayProgressBar.SetActive(false);
        _grayBackGround.localScale = new Vector3(0.01f, 1f, 1f);
        yield return new WaitForSeconds(_initialDelay);

         Sequence tetrisSequence = DOTween.Sequence().
            Append(_tetrisImage.DOPunchScale(new Vector3(0.3f, .6f, .3f), .5f)).
            Append(_tetrisImage.DOScale(new Vector3(.1f, .1f, .1f), .5f));
            

        yield return tetrisSequence.WaitForCompletion();

        _itemImage.gameObject.SetActive(true);
        _tetrisImage.gameObject.SetActive(false);

        Sequence itemSequence = DOTween.Sequence().
            Append(_itemImage.DOScale(new Vector3(1f, 1f, 1f), .5f)).
            Append(_itemImage.DOPunchScale(new Vector3(0.3f, .6f, .3f), .5f));

        yield return itemSequence.WaitForCompletion();

        _grayBackGround.DOScale(new Vector3(1f, 1f, 1f), .5f);

        for (int i = 0; i < _draggableImages.Length; i++)
        {
            RectTransform img = _draggableImages[i];
            Vector3 pos = _draggableTargetPositions[i];
            img.gameObject.SetActive(true);
            Sequence imageSequence = DOTween.Sequence().
                Append(img.DOAnchorPos(pos, 0.5f)).
                Append(img.DOPunchScale(new Vector3(0.2f, .3f, .2f), .5f));
        }

        yield return new WaitForSeconds(.5f);
        
        _grayProgressBar.SetActive(true);
    }
}
