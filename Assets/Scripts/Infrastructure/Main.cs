using Audio;
using Infrastructure.StateMachines.Main;
using UI;
using UI.LoadingScreen;

namespace Infrastructure
{
    public class Main
    {
        public readonly MainStateMachine StateMachine;
        
        public Main(SceneLoader sceneLoader, LoadingScreenController loadingScreenController, AudioManager audioManager)
        {
            StateMachine = new MainStateMachine(sceneLoader, loadingScreenController, audioManager);
        }
    }
}