using Controllers;
using Infrastructure.Services.Input;
using Infrastructure.States;
using Interfaces;
using UniRx;

namespace UI.Game
{
    public class PauseWindowController : IInit, IDispose
    {
        private readonly TimeController timeController;
        private readonly MainStateMachine mainStateMachine;
        private readonly IInputService inputService;
        private readonly PauseWindowView pauseWindowView;
        
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        
        public PauseWindowController(TimeController timeController, MainStateMachine mainStateMachine, IInputService inputService, PauseWindowView pauseWindowView)
        {
            this.timeController = timeController;
            this.mainStateMachine = mainStateMachine;
            this.inputService = inputService;
            this.pauseWindowView = pauseWindowView;
        }

        public void Init()
        {
            SubscribeToClicks();
            inputService.PauseStream.Subscribe(OnPauseInput).AddTo(disposables);
        }

        public void Dispose() => disposables.Dispose();

        private void OnPauseInput(bool performed)
        {
            if (!timeController.IsPaused)
                Show();
            else
                Hide();
        }

        private void Show()
        {
            timeController.Pause();
            pauseWindowView.Show(null);
        }

        private void Hide() => pauseWindowView.Hide(timeController.Unpause);

        private void Continue() => Hide();

        private void OpenSettings()
        {
            //TODO: Open settings window
        }

        private void Exit()
        {
            timeController.Unpause();
            mainStateMachine.Enter<LoadMenuState>();
        }

        private void SubscribeToClicks()
        {
            pauseWindowView.ContinueButton.OnClickAsObservable.Subscribe(OnContinueClick).AddTo(disposables);
            pauseWindowView.SettingsButton.OnClickAsObservable.Subscribe(OnSettingsClick).AddTo(disposables);
            pauseWindowView.ExitButton.OnClickAsObservable.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnContinueClick(Unit unit) => Continue();

        private void OnSettingsClick(Unit unit) => OpenSettings();

        private void OnExitClick(Unit unit) => Exit();
    }
}