using Infrastructure.StateMachines.GameStateMachine;
using Infrastructure.StateMachines.GameStateMachine.States;
using Infrastructure.StateMachines.MainStateMachine;
using Infrastructure.States;
using UniRx;

namespace UI.Game.WinWindow
{
    public class WinWindowController : WindowControllerBase
    {
        private readonly MainStateMachine mainStateMachine;
        private readonly GameStateMachine gameStateMachine;
        private readonly WinWindowView winWindowView;
        
        public WinWindowController(MainStateMachine mainStateMachine, GameStateMachine gameStateMachine, WinWindowView winWindowView)
        {
            this.mainStateMachine = mainStateMachine;
            this.gameStateMachine = gameStateMachine;
            this.winWindowView = winWindowView;
        }
        
        public override void Init()
        {
            SubscribeToClicks();
        }

        public override void Show() => winWindowView.AnimateShow(null);

        public override void Hide() => winWindowView.AnimateHide(null);
        

        private void NextLevel()
        {
            gameStateMachine.Enter<LoadLevelState, int>(gameStateMachine.NextLevelIndex);
        }

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
            winWindowView.NextLevelButton.OnClickAsObservable.Subscribe(OnNextLevelClick).AddTo(disposables);
            winWindowView.ExitButton.OnClickAsObservable.Subscribe(OnExitClick).AddTo(disposables);
            winWindowView.RestartButton.OnClickAsObservable.Subscribe(OnRestartClick).AddTo(disposables);
        }

        private void OnNextLevelClick(Unit unit) => NextLevel();

        private void OnRestartClick(Unit unit) => Restart();

        private void OnExitClick(Unit unit) => Exit();
    }
}