using UnityEngine;

namespace Audio.Data
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/Audio/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        public float FadeTime;
        public float MenuBackgroundFadeTime;
        public float GameBackgroundFadeTime;
        public float AmbientFadeTime;
    }
}