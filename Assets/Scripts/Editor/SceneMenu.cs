using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    private const string BOOTSTRAP_SCENE_PATH = "Assets/Scenes/Bootstrap.unity";
    private const string MAIN_MENU_PATH =  "Assets/Scenes/Menu.unity";
    private const string GAME_SCENE_PATH = "Assets/Scenes/Level.unity";

    [MenuItem("Scene/Open Menu Scene")]
    private static void OpenMainMenuScene() => OpenScene(MAIN_MENU_PATH);

    [MenuItem("Scene/Open Game Scene")]
    private static void OpenGameScene() => OpenScene(GAME_SCENE_PATH);
        
    [MenuItem("Scene/Open Bootstrap Scene")]
    private static void OpenRoguelikeGameScene() => OpenScene(BOOTSTRAP_SCENE_PATH);

    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(sceneName);
    }
}