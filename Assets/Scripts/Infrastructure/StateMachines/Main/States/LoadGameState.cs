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
            
            stateMachine.Enter<GameState>();
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