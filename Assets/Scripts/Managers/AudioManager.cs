using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using Enums;
using Infrastructure.AssetManagement;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;
using AudioSettings = Data.AudioSettings;

namespace Managers
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [Foldout("Mixer")][SerializeField] private AudioMixer mixer;
    
        [Foldout("Audio Sources")][SerializeField] private AudioSource menuSource;
        [Foldout("Audio Sources")][SerializeField] private AudioSource gameSource;
        [Foldout("Audio Sources")][SerializeField] private AudioSource ambientSource;
        [Foldout("Audio Sources")][SerializeField] private AudioSource backgroundSource;

        [SerializeField] private AudioHolder audioHolder;
        [SerializeField] private AudioSettings audioSettings;
        
        private IAssetProvider assetProvider;
        
        public void Construct(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public async UniTask WarmUpMenu() //TODO: Separate control and loading logic
        {
            gameSource.Stop();
            gameSource.clip = null;
            ambientSource.Stop();
            ambientSource.clip = null;
            await audioHolder.LoadMenuBackgroundMusic(assetProvider);
            await audioHolder.LoadMenuSounds(assetProvider);
        }

        public async UniTask WarmUpGame() //TODO: Separate control and loading logic
        {
            menuSource.Stop();
            menuSource.clip = null;
            await audioHolder.LoadGameBackgroundMusic(assetProvider);
            await audioHolder.LoadGameSounds(assetProvider);
        }

        //TODO: Make Play3DSound method with dynamic sound groups pooling
        
        public void Play2DSound(AudioSources source, string soundName)
        {
            if(TryGetAudioSource(source, out AudioSource targetSource) && audioHolder.TryGetSound(soundName, out AudioClip clip))
                targetSource.PlayOneShot(clip);
        }

        public void PlayBackground2DSound(AudioSources source, string soundName, float delayBetweenLoops, bool addFading) //TODO: Separate logic, add public methods to menu and ingame background
        {
            if (TryGetAudioSource(source, out AudioSource targetSource) && audioHolder.TryGetBackgroundSound(soundName, out AudioClip clip))
            {
                if (addFading)
                    PlayLoopWithFading(targetSource, clip, delayBetweenLoops);
                else
                    PlayLoopWithoutFading(targetSource, clip, delayBetweenLoops);
            }
            else
            {
                Debug.LogError($"Can't play {soundName} on {targetSource} source!");
            }
        } 

        public void SetVolume(AudioSources source, float value)
        {
            value = Mathf.Clamp(value, -80f, 20f);
            if(TryGetAudioSource(source, out AudioSource audioSource))
            {
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Volume", value);
            }
            Debug.LogError($"{source.ToString()} not found!");
        }

        public void SetMasterVolume(float value)
        {
            value = Mathf.Clamp(value, -80f, 20f);
            mixer.SetFloat("MasterVolume", value);
        }

        private void PlayLoopWithFading(AudioSource source, AudioClip clip, float delayBetweenLoops)
        {
            source.clip = clip;
            source.volume = 0;
            DOTween.Kill(source);
            Sequence loopSequence = DOTween.Sequence();

            loopSequence.AppendCallback(source.Play);
            loopSequence.Insert(0,source.DOFade(1, audioSettings.FadeTime));
            loopSequence.Insert(0, DOVirtual.DelayedCall(clip.length - audioSettings.FadeTime, () => source.DOFade(0, audioSettings.FadeTime)));
            loopSequence.AppendInterval(audioSettings.FadeTime);
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
