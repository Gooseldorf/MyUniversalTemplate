using System.Collections.Generic;
using Controllers;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Interfaces;
using UI.Game;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadGameState : IStateWithArg<string>
    {
        private readonly SceneLoader sceneLoader;
        private readonly MainStateMachine stateMachine;
        private readonly GameFactory gameFactory;
        private readonly IInputService inputService;

        private List<IDispose> disposables;

        public LoadGameState(MainStateMachine stateMachine, SceneLoader sceneLoader, GameFactory gameFactory, IInputService input)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
            this.gameFactory = gameFactory;
            inputService = input;
        }

        public void Enter(string sceneName) => sceneLoader.Load(sceneName, OnLoad);

        private async void OnLoad()
        {
            TimeController timeController = new TimeController();
            
            GameObject environment = await gameFactory.CreateEnvironment();
            
            PlayerView playerView = await gameFactory.CreatePlayer();
            playerView.transform.position = GameObject.Find("InitialPoint").transform.position; //TODO: not here
            PlayerController playerController = new PlayerController(inputService);
            playerView.Init(inputService);
            
            
            HUDView hudView = await gameFactory.CreateHUD();
            HUDController hudController = new HUDController(hudView);
            disposables.Add(hudController);
            
            PauseWindowView pauseWindowView = await gameFactory.CreatePauseWindow();
            PauseWindowController pauseWindowController = new PauseWindowController(timeController, stateMachine, inputService, pauseWindowView);
            pauseWindowController.Init();
            disposables.Add(pauseWindowController);
            
            stateMachine.Enter<GameState, string>("Level_1");
        }

        public void Exit()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
        }
    }
}