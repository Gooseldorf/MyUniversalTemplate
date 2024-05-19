using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public string PlayerAddress;
        public Vector3 PlayerStartPosition;
    }
}