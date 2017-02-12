using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameTools : EditorWindow   {


    static int TutorialComplete = PlayerPrefs.GetInt(Constants.TUTORIAL_SAVE_ID);

    static int CollectedDust = PlayerPrefs.GetInt(Constants.PROGRESS_SAVE_ID);

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

        GUILayout.BeginHorizontal();
        GUILayout.Label("COLLECTED DUST");
        PlayerPrefs.SetInt(Constants.PROGRESS_SAVE_ID, CollectedDust = EditorGUILayout.IntField(CollectedDust));

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("TUTORIAL COMPLETE");
        PlayerPrefs.SetInt(Constants.TUTORIAL_SAVE_ID, TutorialComplete = EditorGUILayout.IntField(TutorialComplete));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            TutorialComplete = 0;
            CollectedDust = 0;
            this.Repaint();
        }
    }
}
