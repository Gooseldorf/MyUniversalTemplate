using Controllers;
using Infrastructure.Services.Input;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using UI.Base;
using UniRx;

namespace UI.Game.PauseWindow
{
    public class PauseWindowController : WindowControllerBase
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ITimeController timeController;
        private readonly IInputService inputService;
        private readonly PauseWindowView pauseWindowView;
        
        public PauseWindowController(PauseWindowView pauseWindowView, GameStateMachine gameStateMachine, ITimeController timeController, IInputService inputService)
        {
            this.gameStateMachine = gameStateMachine;
            this.timeController = timeController;
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
            gameStateMachine.Enter<QuitToMenuState>();
        }

        private void SubscribeToClicks()
        {
            pauseWindowView.ContinueButton.OnClickStream.Subscribe(OnContinueClick).AddTo(disposables);
            pauseWindowView.SettingsButton.OnClickStream.Subscribe(OnSettingsClick).AddTo(disposables);
            pauseWindowView.ExitButton.OnClickStream.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnContinueClick(Unit unit) => Continue();

        private void OnSettingsClick(Unit unit) => OpenSettings();

        private void OnExitClick(Unit unit) => Exit();
    }
}