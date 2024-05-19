using Controllers;

namespace Infrastructure.StateMachines.Game.States
{
    public class LoseState : IStateNoArg
    {
        private readonly GameStateMachine gameStateMachine;

        public LoseState(GameStateMachine gameStateMachine)
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