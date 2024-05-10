namespace Infrastructure.StateMachines.Game.States
{
    public class WinState : IStateNoArg
    {
        private readonly GameStateMachine gameStateMachine;

        public WinState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            
        }
    }
}