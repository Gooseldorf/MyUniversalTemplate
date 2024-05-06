using Infrastructure;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    private static readonly string BootstrapScenePath = $"Assets/Scenes/{Constants.BOOTSTRAP_SCENE_NAME}.unity";
    private static readonly string MainMenuPath =  $"Assets/Scenes/{Constants.MENU_SCENE_NAME}.unity";
    private static readonly string GameScenePath = $"Assets/Scenes/{Constants.GAME_SCENE_NAME}.unity";

    [MenuItem("Scene/Open Menu Scene")]
    private static void OpenMainMenuScene() => OpenScene(MainMenuPath);

    [MenuItem("Scene/Open Game Scene")]
    private static void OpenGameScene() => OpenScene(GameScenePath);
        
    [MenuItem("Scene/Open Bootstrap Scene")]
    private static void OpenRoguelikeGameScene() => OpenScene(BootstrapScenePath);

    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(sceneName);
    }
}