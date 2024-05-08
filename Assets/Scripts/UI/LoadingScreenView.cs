using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace UI
{
    public class LoadingScreenView : PanelViewBase
    {
        private const float FADE_DURATION = 0.5f; //TODO: UIAnimationDataHolder
        
        public Canvas Canvas;
        public CanvasGroup CanvasGroup;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void ShowLoadingScreen(Action onComplete)
        {
            DOTween.Kill(this);
            Canvas.gameObject.SetActive(true);
            CanvasGroup.DOFade(1, FADE_DURATION).OnComplete(() =>
            {
                CanvasGroup.interactable = true;
                onComplete?.Invoke();
            }).SetUpdate(true).SetTarget(this);
        }
        
        public void HideLoadingScreen(Action onComplete)
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
