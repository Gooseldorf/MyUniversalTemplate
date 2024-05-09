using Infrastructure.StateMachines.Game;

namespace Infrastructure.StateMachines.Main.States
{
    public class GameState: IStateWithArg<string>
    {
        public void Enter(string arg)
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
        }

        public void Exit()
        {
            
        }
    }
}