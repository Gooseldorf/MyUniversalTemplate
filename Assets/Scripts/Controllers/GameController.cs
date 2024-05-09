using Infrastructure.StateMachines.MainStateMachine;
using Interfaces;

namespace Controllers
{
    public class GameController : IGameController
    {
        private readonly MainStateMachine mainStateMachine;

        public GameController(MainStateMachine mainStateMachine)
        {
            this.mainStateMachine = mainStateMachine;
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