namespace Infrastructure.StateMachines.Game.States
{
    public class StartState : IStateNoArg
    {
        private readonly GameStateMachine gameStateMachine;

        public StartState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            gameStateMachine.Enter<LevelState>();
        }

        public void Exit()
        {
            
        }
    }
}