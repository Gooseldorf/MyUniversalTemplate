using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.AssetManagement;
using Infrastructure.DI;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using UnityEngine;

namespace Infrastructure.StateMachines.Game.States
{
    public class StartState : IStateWithArg<int>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly MainStateMachine mainStateMachine;
        private GameInstaller gameInstaller;
        private IAssetProvider assetProvider;

        public StartState(GameStateMachine gameStateMachine, MainStateMachine mainStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.mainStateMachine = mainStateMachine;
        }
        
        public async void Enter(int levelIndex)
        {
            gameStateMachine.CurrentLevelIndex = levelIndex;
            gameStateMachine.NextLevelIndex = levelIndex + 1;
            if(gameInstaller == null)
                gameInstaller = Object.FindObjectOfType<GameInstaller>();
            IGameController gameController = gameInstaller.Resolve<IGameController>();
            LevelData levelData = await GetCurrentLevelData();
            mainStateMachine.Enter<GameState>();
            gameStateMachine.Enter<LevelState>();
            gameController.Play(levelData);
            Debug.Log($"{levelIndex} level started");
        }

        private async UniTask<LevelData> GetCurrentLevelData()
        {
            if (assetProvider == null)
                assetProvider = gameInstaller.Resolve<IAssetProvider>();
            return await assetProvider.LoadAddressable<LevelData>($"Level_{gameStateMachine.CurrentLevelIndex}");
        }

        public void Exit()
        {
            
        }
    }
}