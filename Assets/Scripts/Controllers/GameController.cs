using Infrastructure.StateMachines.Game;

namespace Controllers
{
    public class GameController : IGameController
    {
        private GameStateMachine gameStateMachine;
        
        public GameController(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Init()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}