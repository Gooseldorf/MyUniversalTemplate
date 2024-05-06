using Infrastructure;
using Infrastructure.States;
using Interfaces;
using UniRx;
using UnityEngine;

namespace UI.Game
{
    public class PauseWindowController : IInit, IDispose
    {
        private readonly MainStateMachine mainStateMachine;
        private readonly PauseWindowView pauseWindowView;
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        
        public PauseWindowController(MainStateMachine mainStateMachine, PauseWindowView pauseWindowView)
        {
            this.pauseWindowView = pauseWindowView;
            this.mainStateMachine = mainStateMachine;
        }

        public void Init()
        {
            SubscribeToClicks();
        }

        public void Dispose()
        {
            disposables.Dispose();
        }

        public void Show()
        {
            Time.timeScale = 0;
            pauseWindowView.Show();
        }

        public async void Hide()
        {
            await pauseWindowView.Hide();
            Time.timeScale = 1;
        }
        
        private void SubscribeToClicks()
        {
            pauseWindowView.ContinueButton.OnClickAsObservable.Subscribe(OnContinueClick).AddTo(disposables);
            pauseWindowView.SettingsButton.OnClickAsObservable.Subscribe(OnSettingsClick).AddTo(disposables);
            pauseWindowView.ExitButton.OnClickAsObservable.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnContinueClick(Unit unit) => Continue();

        private void Continue()
        {
            mainStateMachine.Enter<LoadLevelState, string>(Constants.GAME_SCENE_NAME);
        }

        private void OnSettingsClick(Unit unit) => OpenSettings();

        private void OpenSettings()
        {
            Debug.Log("Settings");
        }

        private void OnExitClick(Unit unit) => Exit();

        private void Exit()
        {
            mainStateMachine.Enter<LoadMenuState>();
        }
    }
}