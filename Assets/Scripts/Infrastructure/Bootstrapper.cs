using Infrastructure.DI;
using Infrastructure.Factories;
using Infrastructure.StateMachines.Main.States;
using Managers;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper: MonoBehaviour
    {
        private Main main;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private async void Start()
        {
            BootstrapInstaller bootstrapInstaller = FindObjectOfType<BootstrapInstaller>();
            IBootstrapFactory bootstrapFactory = bootstrapInstaller.Resolve<IBootstrapFactory>();

            LoadingScreenView loadingScreenView = await bootstrapFactory.CreateLoadingScreen();
            LoadingScreenController loadingScreenController = new LoadingScreenController(loadingScreenView);

            AudioManager audioManager = await bootstrapFactory.CreateAudioManager();
            
            main = new Main(loadingScreenController, audioManager);
            main.StateMachine.Enter<BootstrapState>();
        }
    }
}