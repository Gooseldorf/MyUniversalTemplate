using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Main;
using Interfaces;

namespace Controllers
{
    public class GameController : IGameController
    {
        private readonly MainStateMachine mainStateMachine;
        private GameStateMachine gameStateMachine;
        
        public GameController(MainStateMachine mainStateMachine, GameStateMachine gameStateMachine)
        {
            this.mainStateMachine = mainStateMachine;
            this.gameStateMachine = gameStateMachine;
        }

        public void Init()
        {
            
        }

        public void Dispose()
        {
            
        }
    }

    public interface IGameController : IInit, IDispose
    {
        
    }
}