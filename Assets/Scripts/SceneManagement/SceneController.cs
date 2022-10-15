using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DropDown))]
public class SceneController : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DropDown script = (DropDown)target;

        GUIContent arrayLabel = new GUIContent("NextScene");
        script.sceneIdx = EditorGUILayout.Popup(arrayLabel, script.sceneIdx, script.scenes);
    }
}
