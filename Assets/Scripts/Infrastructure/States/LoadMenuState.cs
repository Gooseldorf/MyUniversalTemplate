using Controllers;
using Infrastructure.DI;
using Infrastructure.Factories;
using UI;
using UI.Menu;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadMenuState: IStateNoArg
    {
        private readonly SceneLoader sceneLoader;
        private readonly MainStateMachine stateMachine;
        private readonly LoadingScreenController loadingScreenController;
            
        public LoadMenuState(MainStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreenController loadingScreenController)
        {
            this.sceneLoader = sceneLoader;
            this.stateMachine = stateMachine;
            this.loadingScreenController = loadingScreenController;
        }

        public void Enter()
        {
            loadingScreenController?.ShowLoadingScreen(null);
            sceneLoader.Load(Constants.MENU_SCENE_NAME, OnLoad);
        }

        private async void OnLoad()
        {
            MenuInstaller menuInstaller = Object.FindObjectOfType<MenuInstaller>();
            
            IMenuFactory menuFactory = menuInstaller.Resolve<IMenuFactory>();
            
            MenuPanelView menuView = await menuFactory.CreateMenu();
            MenuController controller = new MenuController(stateMachine, menuView);
            
            stateMachine.Enter<MenuState, IMenuController>(controller);
        }

        public void Exit()
        {
            loadingScreenController.HideLoadingScreen(null);
        }
    }
}