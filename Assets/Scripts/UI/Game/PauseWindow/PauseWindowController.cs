using Controllers;
using Infrastructure.Services.Input;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using UI.Base;
using UniRx;

namespace UI.Game.PauseWindow
{
    public class PauseWindowController : WindowControllerBase
    {
        private readonly TimeController timeController;
        private readonly MainStateMachine mainStateMachine;
        private readonly IInputService inputService;
        private readonly PauseWindowView pauseWindowView;
        
        public PauseWindowController(TimeController timeController, MainStateMachine mainStateMachine, IInputService inputService, PauseWindowView pauseWindowView)
        {
            this.timeController = timeController;
            this.mainStateMachine = mainStateMachine;
            this.inputService = inputService;
            this.pauseWindowView = pauseWindowView;
        }

        public override void Init()
        {
            SubscribeToClicks();
            inputService.PauseStream.Subscribe(OnPauseInput).AddTo(disposables);
        }

        public override void Show()
        {
            timeController.Pause();
            pauseWindowView.AnimateShow(null);
        }

        public override void Hide() => pauseWindowView.AnimateHide(timeController.Unpause);

        private void OnPauseInput(bool performed)
        {
            if (!timeController.IsPaused)
                Show();
            else
                Hide();
        }

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