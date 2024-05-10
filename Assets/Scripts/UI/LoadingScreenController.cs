using System;

namespace UI
{
    public class LoadingScreenController
    {
        private readonly LoadingScreenView loadingScreenView;

        public LoadingScreenController(LoadingScreenView loadingScreenView)
        {
            this.loadingScreenView = loadingScreenView;
        }

        public void ShowLoadingScreen(Action onComplete) => loadingScreenView.ShowLoadingScreen(onComplete);
        public void HideLoadingScreen(Action onComplete) => loadingScreenView.HideLoadingScreen(onComplete);
    }
}