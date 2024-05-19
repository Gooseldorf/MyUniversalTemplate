using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Audio
{
    public interface IAudioManagerFactory
    {
        UniTask<AudioManager> CreateAudioManagerAsync();
    }

    public class AudioManagerFactory : GameObjectFactoryBase, IAudioManagerFactory
    {
        public AudioManagerFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public async UniTask<AudioManager> CreateAudioManagerAsync()
        {
            GameObject audioManager = await InstantiateAddressableAsync("AudioManager");
            audioManager.TryGetComponent(out AudioManager audioManagerComponent);
            audioManagerComponent.Construct(assetProvider);
            return audioManagerComponent;
        }
    }
}