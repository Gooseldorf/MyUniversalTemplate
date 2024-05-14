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
        private int currentLevelIndex;

        public StartState(GameStateMachine gameStateMachine, MainStateMachine mainStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.mainStateMachine = mainStateMachine;
            currentLevelIndex = 0;
        }
        
        public async void Enter(int levelIndex)
        {
            currentLevelIndex = levelIndex;
            gameStateMachine.NextLevelIndex = currentLevelIndex + 1;
            if(gameInstaller == null)
                gameInstaller = Object.FindObjectOfType<GameInstaller>();
            IGameController gameController = gameInstaller.Resolve<IGameController>();
            LevelData levelData = await GetLevelDataByIndex();
            mainStateMachine.Enter<GameState>();
            gameStateMachine.Enter<LevelState>();
            gameController.Play(levelData);
            Debug.Log($"{levelIndex} level started");
        }

        private async UniTask<LevelData> GetLevelDataByIndex()
        {
            if (assetProvider == null)
                assetProvider = gameInstaller.Resolve<IAssetProvider>();
            return await assetProvider.LoadAddressable<LevelData>($"Level_{currentLevelIndex}");
        }

        public void Exit()
        {
            
        }
    }
}