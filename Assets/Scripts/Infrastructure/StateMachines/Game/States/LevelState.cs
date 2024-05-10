namespace Infrastructure.StateMachines.Game.States
{
    public class LevelState : IStateNoArg
    {
        private readonly GameStateMachine gameStateMachine;

        public LevelState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}