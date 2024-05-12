using System.Threading.Tasks;
using Controllers;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using UI;

namespace Infrastructure.StateMachines.Game.States
{
    public class StartState : IStateWithArg<GameController>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly MainStateMachine mainStateMachine;
        private readonly LoadingScreenController loadingScreenController;

        public StartState(GameStateMachine gameStateMachine, MainStateMachine mainStateMachine, LoadingScreenController loadingScreenController)
        {
            this.gameStateMachine = gameStateMachine;
            this.mainStateMachine = mainStateMachine;
            this.loadingScreenController = loadingScreenController;
        }
        
        public async void Enter(GameController gameController)
        {
            await Task.Delay(2000); // TODO: wait for loading screen to hide
            gameController.Play();
            mainStateMachine.Enter<GameState>();
            gameStateMachine.Enter<LevelState>();
        }

        public void Exit()
        {
            
        }
    }
}