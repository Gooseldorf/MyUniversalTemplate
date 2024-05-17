using Audio.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Infrastructure.AssetManagement;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;
using AudioSettings = Audio.Data.AudioSettings;

namespace Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [Foldout("Mixer")][SerializeField] private AudioMixer mixer;
    
        [Foldout("Audio Sources")][SerializeField] private AudioSource menuSource;
        [Foldout("Audio Sources")][SerializeField] private AudioSource gameSource;
        [Foldout("Audio Sources")][SerializeField] private AudioSource ambientSource;
        [Foldout("Audio Sources")][SerializeField] private AudioSource backgroundSource;

        [Foldout("Settings")][SerializeField] private MenuAudioSettings menuAudioSettings;
        [Foldout("Settings")] [SerializeField] private GameAudioSettings gameAudioSettings;
        [Foldout("Settings")] [SerializeField] private AmbientAudioSettings ambientAudioSettings;

        [Foldout("Settings")][SerializeField] private AudioSettings generalAudioSettings;

        [SerializeField] private AudioHolder audioHolder;
        
        private IAssetProvider assetProvider;
        
        public void Construct(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public async UniTask<bool> WarmUpMenu() 
        {
            StopAndClearGameSounds();
            await audioHolder.LoadMenuBackgroundMusic(assetProvider);
            await audioHolder.LoadMenuSounds(assetProvider);
            return true;
        }

        public async UniTask WarmUpGame() 
        {
            StopAndClearMenuSounds();
            await audioHolder.LoadGameBackgroundMusic(assetProvider);
            await audioHolder.LoadGameSounds(assetProvider);
        }

        public void PlayMenuBackground()
        {
            PlayBackground2DSound(
                AudioSources.Background, 
                menuAudioSettings.BackgroundMusicName, 
                menuAudioSettings.DelayBetweenBackgroundLoops, 
                menuAudioSettings.IsFadeBackground,
                menuAudioSettings.BackgroundFadeTime);
        } //TODO: Move play logic to AudioPlayer.cs?
        
        public void PlayGameBackground()
        {
            PlayBackground2DSound(
                AudioSources.Background, 
                gameAudioSettings.BackgroundMusicName, 
                gameAudioSettings.DelayBetweenBackgroundLoops, 
                gameAudioSettings.IsFadeBackground,
                gameAudioSettings.BackgroundFadeTime);
            
            PlayBackground2DSound(
                AudioSources.Ambient,
                ambientAudioSettings.BackgroundSoundName, 
                ambientAudioSettings.DelayBetweenBackgroundLoops, 
                ambientAudioSettings.IsFadeBackground,
                ambientAudioSettings.BackgroundFadeTime);
        }

        public void PlayGame2DSound(string soundKey) => Play2DSound(AudioSources.Game, soundKey);

        public void PlayMenu2DSound(string soundKey) => Play2DSound(AudioSources.Menu, soundKey);

        public void SetSourceVolume(AudioSources source, float value)
        {
            value = Mathf.Clamp(value, -80f, 20f);
            if(TryGetAudioSource(source, out AudioSource audioSource))
            {
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Volume", value);
            }
            Debug.LogError($"{source.ToString()} not found!");
        } //TODO: Move volume logic to VolumeController.cs?

        public void SetMasterVolume(float value)
        {
            value = Mathf.Clamp(value, -80f, 20f);
            mixer.SetFloat("MasterVolume", value);
        }

        //TODO: Make Play3DSound method with dynamic sound groups pooling

        private void Play2DSound(AudioSources source, string soundKey)
        {
            if(TryGetAudioSource(source, out AudioSource targetSource) && audioHolder.TryGetSound(soundKey, out AudioClip clip))
                targetSource.PlayOneShot(clip);
        }

        private void PlayBackground2DSound(AudioSources source, string soundName, float delayBetweenLoops, bool addFading, float fadeTime)
        {
            if (TryGetAudioSource(source, out AudioSource targetSource) && audioHolder.TryGetBackgroundSound(soundName, out AudioClip clip))
            {
                if (addFading)
                    PlayLoopWithFading(targetSource, clip, delayBetweenLoops, fadeTime);
                else
                    PlayLoopWithoutFading(targetSource, clip, delayBetweenLoops);
            }
            else
            {
                Debug.LogError($"Can't play {soundName} on {targetSource} source!");
            }
        }

        private void StopAndClearMenuSounds()
        {
            menuSource.Stop();
            menuSource.clip = null;
        }

        private void StopAndClearGameSounds()
        {
            gameSource.Stop();
            gameSource.clip = null;
            ambientSource.Stop();
            ambientSource.clip = null;
        }

        private void PlayLoopWithFading(AudioSource source, AudioClip clip, float delayBetweenLoops, float fadeTime)
        {
            source.clip = clip;
            source.volume = 0;
            DOTween.Kill(source);
            Sequence loopSequence = DOTween.Sequence();

            loopSequence.AppendCallback(source.Play);
            loopSequence.Insert(0,source.DOFade(1, fadeTime));
            loopSequence.Insert(0, DOVirtual.DelayedCall(clip.length - fadeTime, () => source.DOFade(0, fadeTime)));
            loopSequence.AppendInterval(fadeTime);
            loopSequence.AppendCallback(source.Stop);
            loopSequence.AppendInterval(delayBetweenLoops);

            loopSequence.SetUpdate(true).SetTarget(source);
            loopSequence.SetLoops(-1, LoopType.Restart);
            
            loopSequence.Play();
        }

        private void PlayLoopWithoutFading(AudioSource source, AudioClip clip, float delayBetweenLoops)
        {
            source.clip = clip;
            DOTween.Kill(source);
            Sequence loopSequence = DOTween.Sequence();
            
            loopSequence.AppendCallback(source.Play);
            loopSequence.AppendInterval(clip.length);
            loopSequence.AppendCallback(source.Stop);
            loopSequence.AppendInterval(delayBetweenLoops);

            loopSequence.SetUpdate(true).SetTarget(source);
            loopSequence.SetLoops(-1, LoopType.Restart);
            
            loopSequence.Play();
        }

        private bool TryGetAudioSource(AudioSources sourceEnum, out AudioSource source)
        {
            switch (sourceEnum)
            {
                case AudioSources.Menu:
                    source = menuSource;
                    return true;
                case AudioSources.Game:
                    source = gameSource;
                    return true;
                case AudioSources.Ambient:
                    source = ambientSource;
                    return true;
                case AudioSources.Background:
                    source = backgroundSource;
                    return true;
                default:
                    Debug.LogError($"{sourceEnum.ToString()} not found!");
                    source = null;
                    return false;
            }
        }
    }
}
