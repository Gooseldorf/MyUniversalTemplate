using Audio;
using Controllers;
using Enums;
using Infrastructure.DI;
using Infrastructure.Factories;
using Managers;
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
            sceneLoader.Load(Constants.MENU_SCENE_NAME, OnLoad);
        }

        private async void OnLoad()
        {
            await audioManager.WarmUpMenu();
            MenuInstaller menuInstaller = Object.FindObjectOfType<MenuInstaller>();
            
            IMenuFactory menuFactory = menuInstaller.Resolve<IMenuFactory>();
            
            MenuPanelView menuView = await menuFactory.CreateMenu();
            MenuController controller = new MenuController(stateMachine, menuView);
            
            stateMachine.Enter<MenuState, IMenuController>(controller);
        }

        public void Exit()
        {
            audioManager.PlayBackground2DSound(AudioSources.Background, "MenuBackground", 3, true);
            loadingScreenController.HideLoadingScreen(null);
            Resources.UnloadUnusedAssets();
        }
    }
}