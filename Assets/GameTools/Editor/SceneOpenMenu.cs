using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneOpenMenu : EditorWindow
{
    static readonly string SCENE_PATH = "Assets/ChainPuzzle/Scenes";


    [MenuItem("Scene/Loading", false, 1500)]
    static void OpenLoading()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene($"{SCENE_PATH}/Loading.unity");
    }

    [MenuItem("Scene/InGame", false, 1500)]
    static void OpenInGame()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene($"{SCENE_PATH}/Main.unity");
    }

    [MenuItem("Scene/Home", false, 1500)]
    static void OpenHome()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene($"{SCENE_PATH}/Home.unity");
    }
}