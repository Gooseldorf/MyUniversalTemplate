using NaughtyAttributes;
using UnityEngine;

namespace Audio.Data
{
    [CreateAssetMenu(fileName = nameof(GameAudioSettings), menuName = "ScriptableObjects/Audio/GameAudioSettings")]
    public class GameAudioSettings : ScriptableObject
    {
        public string BackgroundMusicName;
        [MinValue(0)] public float DelayBetweenBackgroundLoops;
        public bool IsFadeBackground;
        [MinValue(0)] public float BackgroundFadeTime;
    }
}