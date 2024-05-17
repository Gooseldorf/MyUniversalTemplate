using Audio;
using Controllers;
using Cysharp.Threading.Tasks;
using Enums;
using Infrastructure.DI;
using UI;
using UI.Menu;
using UnityEngine;

namespace Infrastructure.StateMachines.Main.States
{
    public class LoadMenuState: IStateNoArg
    {
        private readonly SceneLoader sceneLoader;
        private readonly MainStateMachine stateMachine;
        private readonly LoadingScreenController loadingScreenController;
        private readonly AudioManager audioManager;

        public LoadMenuState(MainStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreenController loadingScreenController, AudioManager audioManager)
        {
            this.sceneLoader = sceneLoader;
            this.stateMachine = stateMachine;
            this.loadingScreenController = loadingScreenController;
            this.audioManager = audioManager;
        }

        public void Enter()
        {
            loadingScreenController?.ShowLoadingScreen(null);
            sceneLoader.Load(Constants.MENU_SCENE_NAME, OnMenuSceneLoaded);
        }

        private async void OnMenuSceneLoaded()
        {
            MenuInstaller menuInstaller = Object.FindObjectOfType<MenuInstaller>();
            
            UniTask<bool> warmAudioTask = audioManager.WarmUpMenu(); // Load sounds for menu
            UniTask<MenuController> createMenuControllerTask = CreateMenuController(menuInstaller.Resolve<IMenuFactory>()); // create menu controller
            
            var (audioManagerLoaded, menuController) = await UniTask.WhenAll(warmAudioTask, createMenuControllerTask); 

            stateMachine.Enter<MenuState, IMenuController>(menuController);
        }

        public void Exit()
        {
            Resources.UnloadUnusedAssets();
            audioManager.PlayMenuBackground();
            loadingScreenController.HideLoadingScreen(null);
        }

        private async UniTask<MenuController> CreateMenuController(IMenuFactory menuFactory)
        {
            MenuController menuController = await menuFactory.CreateMenu(stateMachine);
            return menuController;
        }
    }
}