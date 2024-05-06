using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI.Menu;
using UnityEngine;

namespace UI.Game
{
    public class PauseWindowView : WindowViewBase
    {
        public Canvas Canvas;
        public CanvasGroup CanvasGroup;
        public MenuButtonView ContinueButton;
        public MenuButtonView SettingsButton;
        public MenuButtonView ExitButton;
        
        public void Show()
        {
            CanvasGroup.alpha = 1;
            Canvas.enabled = true;
            CanvasGroup.DOFade(1, 1).OnComplete(() =>
            {
                CanvasGroup.interactable = true;
            });
        }
        public UniTask Hide()
        {
            var taskCompletionSource = new UniTaskCompletionSource();
            
            CanvasGroup.interactable = false;
            CanvasGroup.DOFade(0, 1).OnComplete(() =>
            {
                Canvas.enabled = false;
                taskCompletionSource.TrySetResult();
            });
            
            return taskCompletionSource.Task;
        }
    }
}
