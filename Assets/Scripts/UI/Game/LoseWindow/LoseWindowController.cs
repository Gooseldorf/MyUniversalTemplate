using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using UI.Base;
using UniRx;

namespace UI.Game.LoseWindow
{
    public class LoseWindowController: WindowControllerBase
    {
        private readonly MainStateMachine mainStateMachine;
        private readonly GameStateMachine gameStateMachine;
        private readonly LoseWindowView loseWindowView;
        
        public LoseWindowController(MainStateMachine mainStateMachine, GameStateMachine gameStateMachine, LoseWindowView loseWindowView)
        {
            this.mainStateMachine = mainStateMachine;
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
            gameStateMachine.Enter<LoadLevelState, int>(gameStateMachine.CurrentLevelIndex);
        }

        private void Exit()
        {
            mainStateMachine.Enter<LoadMenuState>();
        }

        private void SubscribeToClicks()
        {
            loseWindowView.RestartButton.OnClickAsObservable.Subscribe(OnRestartClick).AddTo(disposables);
            loseWindowView.ExitButton.OnClickAsObservable.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnRestartClick(Unit unit) => Restart();

        private void OnExitClick(Unit unit) => Exit();
    }
}