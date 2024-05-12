using System.Threading.Tasks;
using Controllers;
using Infrastructure.DI;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachines.Game.States
{
    public class StartState : IStateNoArg
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly MainStateMachine mainStateMachine;
        private readonly LoadingScreenController loadingScreenController;

        public StartState(GameStateMachine gameStateMachine, MainStateMachine mainStateMachine, LoadingScreenController loadingScreenController)
        {
            this.gameStateMachine = gameStateMachine;
            this.mainStateMachine = mainStateMachine;
            this.loadingScreenController = loadingScreenController;
        }
        
        public async void Enter()
        {
            await Task.Delay(1000); // TODO: wait for loading screen to hide
            GameInstaller installer = Object.FindObjectOfType<GameInstaller>();
            IGameController gameController = installer.Resolve<IGameController>();
            gameController.Play();
            mainStateMachine.Enter<GameState>();
            gameStateMachine.Enter<LevelState>();
        }

        public void Exit()
        {
            
        }
    }
}