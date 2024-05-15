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
#if UNITY_EDITOR
            loadedSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(menuSoundGroupName);
#else
            loadedSoundsList = await assetProvider.LoadAddressableLabel<AudioClip>(menuSoundGroupName);
#endif
        }

        public async UniTask LoadGameSounds(IAssetProvider assetProvider)
        {
            if(loadedSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedSoundsList);
#if UNITY_EDITOR
            loadedSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(gameSoundGroupName);
#else
            loadedSoundsList = await assetProvider.LoadAddressableLabel<AudioClip>(gameSoundGroupName);
#endif
        }

        public async UniTask LoadMenuBackgroundMusic(IAssetProvider assetProvider)
        {
            if(loadedBackgroundSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedBackgroundSoundsList);
#if UNITY_EDITOR
            loadedBackgroundSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(menuBackgroundMusicGroupName);
#else
            loadedBackgroundSoundsList = await assetProvider.LoadAddressableLabel<AudioClip>(menuBackgroundMusicGroupName);
#endif
        } 
        
        public async UniTask LoadGameBackgroundMusic(IAssetProvider assetProvider)
        {
            if(loadedBackgroundSoundsList.Count > 0)
                assetProvider.UnloadAddressables(loadedBackgroundSoundsList);
#if UNITY_EDITOR
            loadedBackgroundSoundsList = await assetProvider.LoadAddressableGroup<AudioClip>(gameBackgroundMusicGroupName);
#else
            loadedBackgroundSoundsList = await assetProvider.LoadAddressableLabel<AudioClip>(gameBackgroundMusicGroupName);
#endif
        }
    }
}