using Infrastructure.States;
using UI;
using Zenject;

namespace Infrastructure
{
    public class Main
    {
        public readonly MainStateMachine StateMachine;
        
        public Main(LoadingScreenController loadingScreenController)
        {
            StateMachine = new MainStateMachine(new SceneLoader(), loadingScreenController);
        }
    }
}