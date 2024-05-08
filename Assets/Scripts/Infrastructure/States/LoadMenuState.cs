using Controllers;
using Infrastructure.DI;
using Infrastructure.Factories;
using UI;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class LoadMenuState: IStateNoArg
    {
        private readonly SceneLoader sceneLoader;
        private readonly MainStateMachine stateMachine;
            
        [Inject]
        public LoadMenuState(MainStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
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
        }
    }
}