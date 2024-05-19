using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        public int Index;
        public string EnvironmentAddress;
    }
}