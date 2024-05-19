#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    /// <summary>
    /// Loads BootstrapScene on PlayMode entered
    /// </summary>
    public static class LoadSceneOnPlay
    {
        private static int startingSceneIndex = 0;
        private static bool enable = false;

        [InitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            if(enable)
                EditorApplication.playModeStateChanged += OnPlayModeChange;
        }

        private static void OnPlayModeChange(PlayModeStateChange state)
        {
            if(!enable) return;
            
            if (state == PlayModeStateChange.ExitingEditMode)
                startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if (state == PlayModeStateChange.EnteredPlayMode)
                SceneManager.LoadScene(0);

            if (state == PlayModeStateChange.EnteredEditMode)
                EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(startingSceneIndex), OpenSceneMode.Single);
        }
    }
}
#endif