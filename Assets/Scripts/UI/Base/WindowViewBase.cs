using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class WindowViewBase: MonoBehaviour
    {
        protected const float FADE_DURATION = 0.5f; //TODO: UIAnimationDataHolder
        
        [SerializeField] private protected Canvas Canvas;
        [SerializeField] private protected CanvasGroup CanvasGroup;

        public void Show()
        {
            DOTween.Kill(this);
            Canvas.gameObject.SetActive(true);
            CanvasGroup.interactable = true;
        }

        public void Hide()
        {
            DOTween.Kill(this);
            Canvas.gameObject.SetActive(false);
            CanvasGroup.interactable = false;
        }

        public void AnimateShow(Action onComplete)
        {
            DOTween.Kill(this);
            Canvas.gameObject.SetActive(true);
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
                Canvas.gameObject.SetActive(false);
                onComplete?.Invoke();
            }).SetUpdate(true).SetTarget(this);
        }
    }
}