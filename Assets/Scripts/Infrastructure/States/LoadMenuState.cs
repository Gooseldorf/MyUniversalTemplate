using Controllers;
using Infrastructure.Factories;
using UI;
using UI.Menu;
using Zenject;

namespace Infrastructure.States
{
    public class LoadMenuState: IStateNoArg
    {
        private readonly SceneLoader sceneLoader;
        private readonly MainStateMachine stateMachine;
        private readonly MenuFactory menuFactory;
            
        [Inject]
        public LoadMenuState(MainStateMachine stateMachine, SceneLoader sceneLoader, MenuFactory menuFactory)
        {
            this.sceneLoader = sceneLoader;
            this.stateMachine = stateMachine;
            this.menuFactory = menuFactory;
        }

        public void Enter()
        {
            sceneLoader.Load(Constants.MENU_SCENE_NAME, OnLoad);
        }

        private async void OnLoad()
        {
            MenuPanelView menuView = await menuFactory.CreateMenu();
            MenuController controller = new MenuController(stateMachine, menuView);
            
            stateMachine.Enter<MenuState, IMenuController>(controller);
        }

        public void Exit()
        {
        }
    }
}