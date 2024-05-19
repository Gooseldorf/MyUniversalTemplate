using Enums;

namespace Audio
{
    public interface IAudioManager
    {
        void PlayMenuBackground();
        void PlayGameBackground();
        void PlayMenu2DSound(string soundKey);
        void PlayGame2DSound(string soundKey);
        
        void SetSourceVolume(AudioSources source, float value);
        void SetMasterVolume(float value);
    }
}