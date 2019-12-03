using UnityEngine;
using UnityEditor;

public class ExTools : EditorWindow
{
    [MenuItem("Window/ExTools")]
    public static void ShowWindow()
    {
        GetWindow<ExTools>("My Tools");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs Resetted");
        }
    }
}
