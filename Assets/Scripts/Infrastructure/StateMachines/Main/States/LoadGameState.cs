using System.Collections.Generic;
using Controllers;
using Enums;
using Game.Player;
using Infrastructure.DI;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Interfaces;
using Managers;
using UI;
using UI.Game;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.StateMachines.Main.States
{
    public class LoadGameState : IStateWithArg<string>
    {
        private readonly SceneLoader sceneLoader;
        private readonly MainStateMachine stateMachine;
        private readonly LoadingScreenController loadingScreenController;
        private readonly AudioManager audioManager;

        private List<IDispose> disposables;

        public LoadGameState(MainStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreenController loadingScreenController, AudioManager audioManager)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingScreenController = loadingScreenController;
            this.audioManager = audioManager;
        }

        public void Enter(string sceneName)
        {
            loadingScreenController?.ShowLoadingScreen(null);
            sceneLoader.Load(sceneName, OnLoad);
        }

        private async void OnLoad()
        {
            await audioManager.WarmUpGame();
            GameInstaller gameInstaller = Object.FindObjectOfType<GameInstaller>();

            IGameFactory gameFactory = gameInstaller.Resolve<IGameFactory>();
            IInputService inputService = gameInstaller.Resolve<IInputService>();
            
            TimeController timeController = new TimeController();
            
             //TODO: to LevelFactory
            
            PlayerView playerView = await gameFactory.CreatePlayer();
            playerView.transform.position = GameObject.Find("InitialPoint").transform.position; //TODO: not here
            PlayerController playerController = new PlayerController(inputService);
            playerView.Init(inputService);
            
            Canvas mainCanvas = await gameFactory.CreateMainCanvas();
            
            HUDView hudView = await gameFactory.CreateHUD(mainCanvas);
            hudView.transform.SetParent(mainCanvas.transform, false);
            HUDController hudController = new HUDController(hudView);
            
            PauseWindowView pauseWindowView = await gameFactory.CreatePauseWindow(mainCanvas);
            PauseWindowController pauseWindowController = new PauseWindowController(timeController, stateMachine, inputService, pauseWindowView);
            pauseWindowController.Init();
            
            WinWindowView winWindowView = await gameFactory.CreateWinWindow(mainCanvas);
            //WinWindowController winWindowController = new WinWindowController(stateMachine, , winWindowView);
            //winWindowController.Init();
            
            LoseWindowView loseWindowView = await gameFactory.CreateLoseWindow(mainCanvas);
            //LoseWindowController loseWindowController = new LoseWindowController(stateMachine, , loseWindowView);
            //loseWindowController.Init();
            
            stateMachine.Enter<GameState, string>("Level_1");
        }

        public void Exit()
        {
            audioManager.PlayBackground2DSound(AudioSources.Background, "GameBackground", 3, true);
            audioManager.PlayBackground2DSound(AudioSources.Ambient, "Ambient", 3, true);
            loadingScreenController.HideLoadingScreen(null);
            Resources.UnloadUnusedAssets();
        }
    }
}