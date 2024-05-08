using Infrastructure.DI;
using Infrastructure.Factories;
using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper: MonoBehaviour
    {
        public BootstrapInstaller BootstrapInstaller;
        private Main main;
        
        private async void Awake()
        {
            IBootstrapFactory bootstrapFactory = BootstrapInstaller.Resolve<IBootstrapFactory>();

            LoadingScreenView loadingScreenView = await bootstrapFactory.CreateLoadingScreen();
            LoadingScreenController loadingScreenController = new LoadingScreenController(loadingScreenView);
            
            main = new Main(loadingScreenController);
            main.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}