using UnityEngine;
using UnityEditor;

public class ExtendedTools : EditorWindow
{
    [MenuItem("Window/ExTools")]
    public static void ShowWindow()
    {
        GetWindow<ExtendedTools>("My Tools");
    }

    void OnGUI()
    {
        if(GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs Resetted");
        }
    }
}
