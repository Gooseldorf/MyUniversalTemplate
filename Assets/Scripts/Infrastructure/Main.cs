using Infrastructure.StateMachines.MainStateMachine;
using Infrastructure.States;
using Managers;
using UI;

namespace Infrastructure
{
    public class Main
    {
        public readonly MainStateMachine StateMachine;
        
        public Main(LoadingScreenController loadingScreenController, AudioManager audioManager)
        {
            StateMachine = new MainStateMachine(new SceneLoader(), loadingScreenController, audioManager);
        }
    }
}