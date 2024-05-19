using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Game.Environment;
using Game.Player;
using Infrastructure.AssetManagement;
using Infrastructure.DI;
using Infrastructure.Services.Input;
using Interfaces;
using UI.Game;
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
        private List<IDispose> disposes = new List<IDispose>();
        
        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async void Enter(int levelIndex)
        {
            GameInstaller gameInstaller = Object.FindObjectOfType<GameInstaller>();
            
            ITimeController timeController = gameInstaller.Resolve<ITimeController>();
            IAudioManager audioManager = gameInstaller.Resolve<IAudioManager>();
            IAssetProvider assetProvider = gameInstaller.Resolve<IAssetProvider>();
            Updater updater = await CreateUpdater(assetProvider);
            
            PlayerData playerData = await assetProvider.LoadAddressable<PlayerData>("PlayerData");
            HUDData hudData = await assetProvider.LoadAddressable<HUDData>("HUDData");
            
            //CreatePlayer
            IInputService inputService = gameInstaller.Resolve<IInputService>();
            IPlayerFactory playerFactory = gameInstaller.Resolve<IPlayerFactory>();
            PlayerController playerController = await playerFactory.CreatePlayerAsync(playerData, inputService);
            playerController.Init();
            disposes.Add(playerController);
            
            //CreateGameUI
            IGameUIFactory gameUIFactory = gameInstaller.Resolve<IGameUIFactory>();
            Canvas windowsCanvas = await gameUIFactory.CreateWindowsCanvasAsync();

            HUDController hudController = await gameUIFactory.CreateHUDAsync(hudData);
            hudController.Init();
            disposes.Add(hudController);

            PauseWindowController pauseWindowController = await gameUIFactory.CreatePauseWindowAsync(windowsCanvas, gameStateMachine, timeController, inputService);
            pauseWindowController.Init();
            disposes.Add(pauseWindowController);

            WinWindowController winWindowController = await gameUIFactory.CreateWinWindowAsync(windowsCanvas, gameStateMachine);
            winWindowController.Init();
            disposes.Add(winWindowController);

            LoseWindowController loseWindowController = await gameUIFactory.CreateLoseWindowAsync(windowsCanvas, gameStateMachine);
            loseWindowController.Init();
            disposes.Add(loseWindowController);
            
            //Create GameController
            GameController gameController = new GameController(gameStateMachine, winWindowController, loseWindowController, hudController,timeController);
            gameController.Init(disposes);
            gameInstaller.BindAsSingleFromInstance<IGameController, GameController>(gameController);
            
            gameStateMachine.Enter<StartState, int>(gameStateMachine.CurrentLevelIndex);
        }

        private static async Task<Updater> CreateUpdater(IAssetProvider assetProvider)
        {
            GameObject updaterObj = await assetProvider.InstantiateAddressableAsync("Updater");
            Updater updater = updaterObj.GetComponent<Updater>();
            return updater;
        }

        public void Exit()
        {
            
        }
    }
}