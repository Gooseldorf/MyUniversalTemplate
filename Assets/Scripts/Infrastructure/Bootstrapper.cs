using System.Threading.Tasks;
using Audio;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.StateMachines.Main.States;
using Managers;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public sealed class Bootstrapper: MonoBehaviour
    {
        private ILoadingScreenFactory loadingScreenFactory;
        private IAudioManagerFactory audioManagerFactory;
        
        private Main main;

        [Inject]
        private void Construct(IAudioManagerFactory audioManagerFactory, ILoadingScreenFactory loadingScreenFactory)
        {
            this.loadingScreenFactory = loadingScreenFactory;
            this.audioManagerFactory = audioManagerFactory;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private async void Start()
        {
            AudioManager audioManager = await CreateAudioManager();
            LoadingScreenController loadingScreenController = await CreateLoadingScreen();

            main = new Main(loadingScreenController, audioManager);
            main.StateMachine.Enter<BootstrapState>();
        }

        private async UniTask<LoadingScreenController> CreateLoadingScreen()
        {
            await loadingScreenFactory.WarmUpIfNeeded();
            LoadingScreenController loadingScreenController = loadingScreenFactory.CreateLoadingScreen();
            loadingScreenFactory.Clear();
            return loadingScreenController;
        }

        private async UniTask<AudioManager> CreateAudioManager()
        {
            await audioManagerFactory.WarmUpIfNeeded();
            AudioManager audioManager = audioManagerFactory.CreateAudioManager();
            audioManagerFactory.Clear();
            return audioManager;
        }
    }
}