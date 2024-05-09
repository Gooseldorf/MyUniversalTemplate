using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;

namespace Infrastructure.StateMachines.Main.States
{
    public class GameState: IStateNoArg
    {
        private readonly MainStateMachine mainStateMachine;
        private GameStateMachine gameStateMachine;

        public GameState(MainStateMachine mainStateMachine)
        {
            this.mainStateMachine = mainStateMachine;
        }

        public void Enter()
        {
            gameStateMachine = new GameStateMachine(mainStateMachine);
            gameStateMachine.Enter<LoadLevelState, int>(0); //TODO: Get level
        }

        public void Exit()
        {
            //TODO: Dispose GameStateMachine
        }
    }
}