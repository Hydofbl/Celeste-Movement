using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class DropDown : MonoBehaviour
{
    [HideInInspector]
    public int sceneIdx = 0;
    [HideInInspector]
    public string[] scenes;

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