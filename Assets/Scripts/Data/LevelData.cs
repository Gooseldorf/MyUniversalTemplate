using Unity.Mathematics;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string EnvironmentAddress;
        
        public string PlayerAddress;
        public float3 PlayerPosition;
        public float PlayerSpeed;
    }
}