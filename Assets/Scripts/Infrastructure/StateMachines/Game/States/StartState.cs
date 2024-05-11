using System.Threading.Tasks;
using Controllers;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;

namespace Infrastructure.StateMachines.Game.States
{
    public class StartState : IStateWithArg<GameController>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly MainStateMachine mainStateMachine;

        public StartState(GameStateMachine gameStateMachine, MainStateMachine mainStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.mainStateMachine = mainStateMachine;
        }
        
        public async void Enter(GameController gameController)
        {
            await Task.Delay(2000); // TODO: wait for loading screen
            gameController.Play();
            mainStateMachine.Enter<GameState>();
            gameStateMachine.Enter<LevelState>();
        }

        public void Exit()
        {
            
        }
    }
}