using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Foldout("Mixer")][SerializeField] private AudioMixer mixer;
    [Foldout("Mixer")][SerializeField] private AudioMixerGroup menuMixerGroup;
    [Foldout("Mixer")][SerializeField] private AudioMixerGroup gameMixerGroup;
    [Foldout("Mixer")][SerializeField] private AudioMixerGroup ambientMixerGroup;
    [Foldout("Mixer")][SerializeField] private AudioMixerGroup backgroundMixerGroup;
    
    [Foldout("Audio Sources")][SerializeField] private AudioSource menuSource;
    [Foldout("Audio Sources")][SerializeField] private AudioSource gameSource;
    [Foldout("Audio Sources")][SerializeField] private AudioSource ambientSource;
    [Foldout("Audio Sources")][SerializeField] private AudioSource backgroundSource;

    [Foldout("Sounds")][SerializeField] private List<AudioClip> menuSounds = new ();
    [Foldout("Sounds")][SerializeField] private List<AudioClip> inGameSounds = new ();
    [Foldout("Sounds")][SerializeField] private List<AudioClip> ambientSounds = new ();
    [Foldout("Sounds")][SerializeField] private List<AudioClip> backgroundMusic = new ();
    [Foldout("Sounds")][SerializeField] private float fadeTime;

    private void Start()
    {
        CheckSounds();
        CheckSources();
    }

    public void PlaySound(AudioSources source, string clipName)
    {
        AudioClip clip = null;
        switch (source)
        {
            case AudioSources.Menu:
                
                clip = menuSounds.Find(sound => sound.name == clipName);
                menuSource.PlayOneShot(clip);
                break;
            case AudioSources.Game:
                clip = inGameSounds.Find(sound => sound.name == clipName);
                gameSource.PlayOneShot(clip);
                break;
            case AudioSources.Ambient:
                clip = ambientSounds.Find(sound => sound.name == clipName);
                ambientSource.PlayOneShot(clip);
                break;
            case AudioSources.Background:
              
                clip = backgroundMusic.Find(sound => sound.name == clipName);
                backgroundSource.PlayOneShot(clip);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(source), source, null);
        }
        if (clip == null)
        {
            Debug.LogError($"{name} Clip {clipName} not found!");
        }
    }

    public void SetVolume(AudioSources source, float value)
    {
        value = Mathf.Clamp(value, -80f, 20f);

        switch (source)
        {
            case AudioSources.Menu:
                menuMixerGroup.audioMixer.SetFloat("Volume", value);
                break;
            case AudioSources.Game:
                gameMixerGroup.audioMixer.SetFloat("Volume", value);
                break;
            case AudioSources.Ambient:
                ambientMixerGroup.audioMixer.SetFloat("Volume", value);
                break;
            case AudioSources.Background:
                backgroundMixerGroup.audioMixer.SetFloat("Volume", value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(source), source, null);
        }
    }

    public void SetMasterVolume(float value)
    {
        value = Mathf.Clamp(value, -80f, 20f);
        mixer.SetFloat("MasterVolume", value);
    }

    public void PlayLoop(AudioSources source, string clipName, float delayBetweenLoops, bool addFading)
    {
        AudioClip clip = null;
        AudioSource targetSource = null;
        switch (source)
        {
            case AudioSources.Menu:
                targetSource = menuSource;
                clip = menuSounds.Find(sound => sound.name == clipName);
                break;
            case AudioSources.Game:
                targetSource = gameSource;
                clip = inGameSounds.Find(sound => sound.name == clipName);
                break;
            case AudioSources.Ambient:
                targetSource = ambientSource;
                clip = ambientSounds.Find(sound => sound.name == clipName);
                break;
            case AudioSources.Background:
                targetSource = backgroundSource;
                clip = backgroundMusic.Find(sound => sound.name == clipName);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(source), source, null);
        }
        if (clip == null)
        {
            Debug.LogError($"{name} Clip {clipName} not found!");
            return;
        }

        StartCoroutine(addFading ? PlayLoopWithFading(targetSource, clip, delayBetweenLoops) : PlayLoopWithoutFading(targetSource, clip, delayBetweenLoops));
    }

    private IEnumerator PlayLoopWithFading(AudioSource source, AudioClip clip, float delayBetweenLoops)
    {
        source.clip = clip;
        source.volume = 0;
        source.Play();
        float endFadeInTime = Time.unscaledTime + fadeTime;
        float startFadeOutTime = Time.unscaledTime + clip.length - fadeTime;
        while (Time.unscaledTime < endFadeInTime)
        {
            source.volume = Mathf.Lerp(0, 1, (endFadeInTime - Time.unscaledTime) / fadeTime);
            yield return null;
        }

        while (Time.unscaledTime > startFadeOutTime)
        {
            source.volume = Mathf.Lerp(1, 0, (Time.unscaledTime - startFadeOutTime) / fadeTime);
            yield return null;
        }
        yield return new WaitForSeconds(clip.length + delayBetweenLoops);
        
        source.Play();
    }

    private IEnumerator PlayLoopWithoutFading(AudioSource source, AudioClip clip, float delayBetweenLoops)
    {
        source.clip = clip;
        source.Play();
        yield return new WaitForSeconds(clip.length + delayBetweenLoops);
        source.Play();
    }
    
    private void CheckSources()
    {
        if (menuSource == null) Debug.LogWarning($"{name} Menu source is null!");
        if (gameSource == null) Debug.LogWarning($"{name} Game source is null!");
        if (ambientSource == null) Debug.LogWarning($"{name} Ambient source is null!");
        if (backgroundSource == null) Debug.LogWarning($"{name} Background source is null!");
    }

    private void CheckSounds()
    {
        if (menuSounds.Count == 0) Debug.LogWarning($"{name} No audio in menuSounds!");
        if (menuSounds.Count == 0) Debug.LogWarning($"{name} No audio in gameSounds!");
        if (ambientSounds.Count == 0) Debug.LogWarning($"{name} No audio in ambientSounds!");
        if (menuSounds.Count == 0) Debug.LogWarning($"{name} No audio in backgroundSounds!");
    }
}
