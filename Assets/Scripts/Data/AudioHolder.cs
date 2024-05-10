using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using ModestTree;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AudioHolder", menuName = "ScriptableObjects/Audio/AudioHolder")]
    public class AudioHolder : ScriptableObject
    {
        [SerializeField] private string menuSoundGroupName;
        [SerializeField] private string menuBackgroundMusicGroupName;
        [SerializeField] private string gameSoundGroupName;
        [SerializeField] private string gameBackgroundMusicGroupName;

        [NonSerialized] private List<AudioClip> loadedSoundsList = new ();
        [NonSerialized] private List<AudioClip> loadedBackgroundSoundsList = new ();

        public bool TryGetSound(string soundName, out AudioClip result)
        {
            result = loadedSoundsList?.Find(x => x.name == soundName);
            return result != null;
        }

        public bool TryGetBackgroundSound(string soundName, out AudioClip result)
        {
            result = loadedBackgroundSoundsList?.Find(x => x.name == soundName);
            return result != null;
        }

        public async UniTask LoadMenuSounds(IAssetProvider assetProvider)
        {
            if(loadedSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedSoundsList);
            loadedSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(menuSoundGroupName);
        }

        public async UniTask LoadGameSounds(IAssetProvider assetProvider)
        {
            if(loadedSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedSoundsList);
            loadedSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(gameSoundGroupName);
        }

        public async UniTask LoadMenuBackgroundMusic(IAssetProvider assetProvider)
        {
            if(loadedBackgroundSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedBackgroundSoundsList);
            loadedBackgroundSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(menuBackgroundMusicGroupName);
        } 
        
        public async UniTask LoadGameBackgroundMusic(IAssetProvider assetProvider)
        {
            if(loadedBackgroundSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedBackgroundSoundsList);
            loadedBackgroundSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(gameBackgroundMusicGroupName);
        }
    }
}