using Audio;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.StateMachines.Main.States;
using UI;
using UI.LoadingScreen;
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
            AudioManager audioManager = await audioManagerFactory.CreateAudioManagerAsync();
            LoadingScreenController loadingScreenController = await loadingScreenFactory.CreateLoadingScreenAsync();
            SceneLoader sceneLoader = new SceneLoader();
            
            main = new Main(sceneLoader, loadingScreenController, audioManager);
            main.StateMachine.Enter<BootstrapState, bool>(projectSettings.IsLoadMainMenu);
        }
    }
}