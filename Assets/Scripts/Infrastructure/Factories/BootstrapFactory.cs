using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Managers;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class BootstrapFactory : IBootstrapFactory
    {
        private readonly IAssetProvider assetProvider;

        public BootstrapFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public async UniTask<LoadingScreenView> CreateLoadingScreen()
        {
            GameObject loadingScreen =  await assetProvider.InstantiateAddressable("LoadingScreen");
            loadingScreen.TryGetComponent(out LoadingScreenView loadingScreenView);
            return loadingScreenView;
        }

        public async UniTask<AudioManager> CreateAudioManager()
        {
            GameObject audioManager =  await assetProvider.InstantiateAddressable("AudioManager");
            audioManager.TryGetComponent(out AudioManager audioManagerComponent);
            audioManagerComponent.Construct(assetProvider);
            return audioManagerComponent;
        }
    }
}