using Controllers;

namespace Infrastructure.StateMachines.Main.States
{
    public class GameState: IStateWithArg<IGameController>
    {
        //private IGameController gameController;
        
        public GameState()
        {
            
        }

        public void Enter(IGameController gameController)
        {
            //gameController.Init();
        }

        public void Exit()
        {
            //gameController.Dispose();
        }
    }
}