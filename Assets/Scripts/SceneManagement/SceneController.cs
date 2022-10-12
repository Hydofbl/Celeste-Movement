 using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string[] scenes;

    void Awake()
    {
        var sceneNumber = SceneManager.sceneCountInBuildSettings;
        scenes = new string[sceneNumber];
        for (int i = 0; i < sceneNumber; i++)
        {
            scenes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
    }
}
