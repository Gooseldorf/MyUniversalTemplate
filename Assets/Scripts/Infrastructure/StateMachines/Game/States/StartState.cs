using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;

namespace Infrastructure.StateMachines.Game.States
{
    public class StartState : IStateNoArg
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly MainStateMachine mainStateMachine;

        public StartState(GameStateMachine gameStateMachine, MainStateMachine mainStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.mainStateMachine = mainStateMachine;
        }
        
        public void Enter()
        {
            mainStateMachine.Enter<GameState>();
            gameStateMachine.Enter<LevelState>();
        }

        public void Exit()
        {
            
        }
    }
}