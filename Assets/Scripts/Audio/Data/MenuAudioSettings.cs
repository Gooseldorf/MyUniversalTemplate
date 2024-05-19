using NaughtyAttributes;
using UnityEngine;

namespace Audio.Data
{
    [CreateAssetMenu(fileName = nameof(MenuAudioSettings), menuName = "ScriptableObjects/Audio/MenuAudioSettings")]
    public class MenuAudioSettings : ScriptableObject
    {
        public string BackgroundMusicName;
        [MinValue(0)] public float DelayBetweenBackgroundLoops;
        public bool IsFadeBackground;
        [MinValue(0)] public float BackgroundFadeTime;
    }
}
