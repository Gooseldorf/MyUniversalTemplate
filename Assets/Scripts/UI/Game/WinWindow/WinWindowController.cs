using Controllers;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using UI.Base;
using UniRx;

namespace UI.Game.WinWindow
{
    public class WinWindowController : WindowControllerBase
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly WinWindowView winWindowView;
        
        public WinWindowController(GameStateMachine gameStateMachine, WinWindowView winWindowView)
        {
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
            Hide();
            gameStateMachine.Enter<StartState, int>(gameStateMachine.NextLevelIndex);
        }

        private void Restart()
        {
            Hide();
            gameStateMachine.Enter<StartState, int>(gameStateMachine.NextLevelIndex);
        }

        private void Exit()
        {
            gameStateMachine.Enter<QuitToMenuState>();
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