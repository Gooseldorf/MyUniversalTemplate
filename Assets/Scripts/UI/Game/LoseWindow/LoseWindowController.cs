using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using UI.Base;
using UniRx;

namespace UI.Game.LoseWindow
{
    public class LoseWindowController: WindowControllerBase
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly LoseWindowView loseWindowView;
        
        public LoseWindowController(LoseWindowView loseWindowView, GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.loseWindowView = loseWindowView;
        }
        
        public override void Init()
        {
            SubscribeToClicks();
        }

        public override void Show() => loseWindowView.AnimateShow(null);

        public override void Hide() => loseWindowView.AnimateHide(null);
        
        private void Restart()
        {
            Hide();
            gameStateMachine.Enter<StartState, int>(gameStateMachine.CurrentLevelIndex);
        }

        private void Exit()
        {
            gameStateMachine.Enter<QuitToMenuState>();
        }

        private void SubscribeToClicks()
        {
            loseWindowView.RestartButton.OnClickStream.Subscribe(OnRestartClick).AddTo(disposables);
            loseWindowView.ExitButton.OnClickStream.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnRestartClick(Unit unit) => Restart();

        private void OnExitClick(Unit unit) => Exit();
    }
}