using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameTools : EditorWindow   {

    [MenuItem("Foo/GameTools")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GameTools));
    }

    void OnGUI()
    {

       if(TurnSystem.Instance != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(TurnSystem.Instance.GetCurrentTurn().ToString());
            if(GUILayout.Button("Next Turn"))
            {
                TurnSystem.Instance.NextTurn();
            }
        }
    }
}
