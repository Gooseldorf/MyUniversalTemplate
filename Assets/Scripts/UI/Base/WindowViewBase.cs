using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Base
{
    public abstract class WindowViewBase: MonoBehaviour
    {
        private const float FADE_DURATION = 0.5f; //TODO: UIAnimationDataHolder
        
        [SerializeField] private protected GameObject Container;
        [SerializeField] private protected CanvasGroup CanvasGroup;

        public void Show()
        {
            DOTween.Kill(this);
            Container.gameObject.SetActive(true);
            CanvasGroup.interactable = true;
        }

        public void Hide()
        {
            DOTween.Kill(this);
            Container.gameObject.SetActive(false);
            CanvasGroup.interactable = false;
        }

        public void AnimateShow(Action onComplete)
        {
            DOTween.Kill(this);
            Container.gameObject.SetActive(true);
            CanvasGroup.DOFade(1, FADE_DURATION).OnComplete(() =>
            {
                CanvasGroup.interactable = true;
                onComplete?.Invoke();
            }).SetUpdate(true).SetTarget(this);
        }

        public void AnimateHide(Action onComplete)
        {
            DOTween.Kill(this);
            CanvasGroup.interactable = false;
            CanvasGroup.DOFade(0, FADE_DURATION).OnComplete(() =>
            {
                Container.gameObject.SetActive(false);
                onComplete?.Invoke();
            }).SetUpdate(true).SetTarget(this);
        }
    }
}