using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;

namespace Infrastructure.StateMachines.Main.States
{
    public class GameState: IStateNoArg
    {
        public GameState()
        {
            
        }

        public void Enter()
        {
            //TODO: Move to LoadGameState
        }

        public void Exit()
        {
            //TODO: Dispose GameStateMachine
        }
    }
}