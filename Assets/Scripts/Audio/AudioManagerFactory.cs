using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Audio
{
    public interface IAudioManagerFactory
    {
        AudioManager CreateAudioManager();

        UniTask WarmUpIfNeeded();

        void Clear();
    }

    public class AudioManagerFactory : GameObjectFactoryBase, IAudioManagerFactory
    {
        private GameObject audioManagerPrefab;
        
        public AudioManagerFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public override async UniTask WarmUpIfNeeded()
        {
            if(audioManagerPrefab == null)
                audioManagerPrefab = await CachePrefab("AudioManager");
        }

        public override void Clear()
        {
            audioManagerPrefab = null;
        }

        public AudioManager CreateAudioManager()
        {
            GameObject audioManager = Object.Instantiate(audioManagerPrefab);
            audioManager.TryGetComponent(out AudioManager audioManagerComponent);
            audioManagerComponent.Construct(assetProvider);
            return audioManagerComponent;
        }
    }
}