using NaughtyAttributes;
using UnityEngine;

namespace Audio.Data
{
    [CreateAssetMenu(fileName = nameof(AmbientAudioSettings), menuName = "ScriptableObjects/Audio/AmbientAudioSettings")]
    public class AmbientAudioSettings : ScriptableObject
    {
        public string BackgroundSoundName;
        [MinValue(0)] public float DelayBetweenBackgroundLoops;
        public bool IsFadeBackground;
        [MinValue(0)] public float BackgroundFadeTime;
    }
}