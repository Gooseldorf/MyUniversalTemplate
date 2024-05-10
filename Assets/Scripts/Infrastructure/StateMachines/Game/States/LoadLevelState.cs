using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Game.Player;
using Infrastructure.AssetManagement;
using Infrastructure.DI;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.StateMachines.Main;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IStateWithArg<int>
    {
        private readonly GameStateMachine gameStateMachine;

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async void Enter(int levelIndex)
        {
            GameInstaller gameInstaller = Object.FindObjectOfType<GameInstaller>();
            TimeController timeController = new TimeController();

            //Load LevelData by index
            IAssetProvider assetProvider = gameInstaller.Resolve<IAssetProvider>();
            LevelData levelData = await assetProvider.LoadAddressable<LevelData>($"Level_{levelIndex}");
            
            
            //CreateEnvironment
            ILevelFactory levelFactory = gameInstaller.Resolve<ILevelFactory>();
            GameObject environment = await levelFactory.CreateEnvironment(levelData);
            
            
            //CreatePlayer
            IInputService inputService = gameInstaller.Resolve<IInputService>();
            IPlayerFactory playerFactory = gameInstaller.Resolve<IPlayerFactory>();
            PlayerView playerView = await playerFactory.CreatePlayer(levelData);
            PlayerController playerController = new PlayerController(playerView, inputService);
            playerController.Init();
            
            //CreateGameUI
            IGameUIFactory gameUIFactory = gameInstaller.Resolve<IGameUIFactory>();
            await CreateGameUI(gameUIFactory, timeController, inputService);
            
            gameStateMachine.Enter<StartState>();
        }

        private async UniTask CreateGameUI(IGameUIFactory gameUIFactory, TimeController timeController, IInputService inputService)
        {
            Canvas windowsCanvas = await gameUIFactory.CreateWindowsCanvas();
            
            HUDView hudView = await gameUIFactory.CreateHUD();
            HUDController hudController = new HUDController(hudView);
            
            PauseWindowView pauseWindowView = await gameUIFactory.CreatePauseWindow(windowsCanvas);
            PauseWindowController pauseWindowController = new PauseWindowController(gameStateMachine, timeController, inputService, pauseWindowView);
            pauseWindowController.Init();
            
            WinWindowView winWindowView = await gameUIFactory.CreateWinWindow(windowsCanvas);
            WinWindowController winWindowController = new WinWindowController(gameStateMachine, winWindowView);
            winWindowController.Init();
            
            LoseWindowView loseWindowView = await gameUIFactory.CreateLoseWindow(windowsCanvas);
            LoseWindowController loseWindowController = new LoseWindowController(gameStateMachine, loseWindowView);
            loseWindowController.Init();
        }
        
        public void Exit()
        {
            
        }
    }
}