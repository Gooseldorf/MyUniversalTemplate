using Managers;
using UI;
using MainStateMachine = Infrastructure.StateMachines.Main.MainStateMachine;

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