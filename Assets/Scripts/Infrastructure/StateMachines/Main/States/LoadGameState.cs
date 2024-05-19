using System.Collections.Generic;
using Audio;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using Interfaces;
using UI;
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

            GameStateMachine gameStateMachine = new GameStateMachine(stateMachine);
            gameStateMachine.Enter<LoadLevelState, int>(0); //TODO: Get level
        }

        public void Exit()
        {
            audioManager.PlayGameBackground();
            loadingScreenController.HideLoadingScreen(null);
            Resources.UnloadUnusedAssets();
        }
    }
}