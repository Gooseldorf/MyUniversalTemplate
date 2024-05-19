using Controllers;

namespace Infrastructure.StateMachines.Main.States
{
    public class MenuState : IStateWithArg<IMenuController>
    {
        private IMenuController menuController;

        public MenuState() { }

        public void Enter(IMenuController menuController)
        {
            this.menuController = menuController;
            menuController.Init();
        }

        public void Exit()
        {
            menuController.Dispose();
        }
    }
}