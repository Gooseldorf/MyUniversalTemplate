using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "ScriptableObjects/ProjectSettings")]
    public class ProjectSettings : ScriptableObject
    {
        public bool IsLoadMainMenu;
    }
}