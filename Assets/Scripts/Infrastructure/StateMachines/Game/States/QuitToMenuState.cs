using Controllers;
using Infrastructure.DI;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using UnityEngine;

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
            GameInstaller gameInstaller = Object.FindObjectOfType<GameInstaller>();
            IGameController gameController = gameInstaller.Resolve<IGameController>();
            gameController.Dispose();
            mainStateMachine.Enter<LoadMenuState>();
        }

        public void Exit()
        {
            
        }
    }
}