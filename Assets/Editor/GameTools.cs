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

    int _cheatedDust;
    int _enteredDust;
 

    void OnGUI()
    {
        

        GUILayout.BeginHorizontal();
        if (TurnSystem.Instance != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(TurnSystem.Instance.GetCurrentTurn().ToString());
            if(GUILayout.Button("Next Turn"))
            {
                TurnSystem.Instance.NextTurn();
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("COLLECTED DUST");
 


        _enteredDust =  EditorGUILayout.IntField(_enteredDust);

        GUILayout.Label(" Current saved :" + PlayerPrefs.GetInt(Constants.PROGRESS_SAVE_ID));

        if (GUILayout.Button("Set New Amount"))
        {
         PlayerPrefs.SetInt(Constants.PROGRESS_SAVE_ID, _enteredDust);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("TUTORIAL= " +( ( PlayerPrefs.GetInt(Constants.TUTORIAL_SAVE_ID) == 0) ? "NOT COMPLETED" : "COMPLETE"));

        if (PlayerPrefs.GetInt(Constants.TUTORIAL_SAVE_ID) == 0) {
           if(  GUILayout.Button("SET TUTORIAL COMPLETE"))
            {
                PlayerPrefs.SetInt(Constants.TUTORIAL_SAVE_ID, 1);
            }
        } else
        {
            if (GUILayout.Button("SET TUTORIAL UN COMPLETE"))
            {
                PlayerPrefs.SetInt(Constants.TUTORIAL_SAVE_ID, 0);
            }

        }
       // PlayerPrefs.SetInt(Constants.TUTORIAL_SAVE_ID, TutorialComplete = EditorGUILayout.IntField(TutorialComplete));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            TutorialComplete = 0;
            CollectedDust = 0;
            this.Repaint();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (Application.isPlaying)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Identify all units"))
            {
                Unit.AllUnits.ForEach(unit => unit.Identify(null));
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("DUST Cheats");
            _cheatedDust = EditorGUILayout.IntField(_cheatedDust);

            if (GUILayout.Button("Cheat Dust"))
            {
                PlayerInventory.CheatDust(_cheatedDust);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndHorizontal();

    }
}
