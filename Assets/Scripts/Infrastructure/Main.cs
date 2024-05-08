using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
    public class Main
    {
        public readonly MainStateMachine StateMachine;
        
        public Main()
        {
            StateMachine = new MainStateMachine(new SceneLoader());
        }
    }
}