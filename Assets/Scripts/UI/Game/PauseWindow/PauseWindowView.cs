using System;
using DG.Tweening;
using UI.Menu;
using UnityEngine;

namespace UI.Game
{
    public class PauseWindowView : WindowViewBase
    {
        private const float FADE_DURATION = 0.5f; //TODO: UIAnimationDataHolder

        public Canvas Canvas;
        public CanvasGroup CanvasGroup;
        public MenuButtonView ContinueButton;
        public MenuButtonView SettingsButton;
        public MenuButtonView ExitButton;

        public void Show(Action onComplete)
        {
            DOTween.Kill(this);
            Canvas.gameObject.SetActive(true);
            CanvasGroup.DOFade(1, FADE_DURATION).OnComplete(() =>
            {
                CanvasGroup.interactable = true;
                onComplete?.Invoke();
            }).SetUpdate(true).SetTarget(this);
        }
        
        public void Hide(Action onComplete)
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
