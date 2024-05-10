using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;

namespace Infrastructure.StateMachines.Game.States
{
    public class QuitToMenuState : IStateNoArg
    {
        private readonly MainStateMachine mainStateMachine;

        public QuitToMenuState(MainStateMachine mainStateMachine)
        {
            this.mainStateMachine = mainStateMachine;
        }

        public void Enter()
        {
            mainStateMachine.Enter<LoadMenuState>();
        }

        public void Exit()
        {
            
        }
    }
}