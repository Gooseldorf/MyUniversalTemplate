using Audio;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.StateMachines.Main.States;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// DonDestroyOnLoad class, that creates and stores Main instance
    /// </summary>
    public sealed class Bootstrapper: MonoBehaviour //TODO: Parallel asset loading
    {
        [SerializeField] private ProjectSettings projectSettings;
        
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
            SceneLoader sceneLoader = new SceneLoader();
            
            main = new Main(sceneLoader, loadingScreenController, audioManager);
            main.StateMachine.Enter<BootstrapState, bool>(projectSettings.IsLoadMainMenu);
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