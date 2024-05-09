using Enums;

namespace Managers
{
    public interface IAudioManager
    {
        void Play2DSound(AudioSources source, string soundName);
        void SetVolume(AudioSources source, float value);
        void SetMasterVolume(float value);
        void PlayBackground2DSound(AudioSources source, string soundName, float delayBetweenLoops, bool addFading);
    }
}