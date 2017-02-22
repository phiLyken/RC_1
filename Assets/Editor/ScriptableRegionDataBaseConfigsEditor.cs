using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ScriptableRegionDataBaseConfigs))]
public class ScriptableRegionDataBaseConfigsEditor : Editor {


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("SAVES");

        foreach( var config in (target as ScriptableRegionDataBaseConfigs).RegionConfigs)
        {
            bool is_saved = config.IsCompleteInSave();

            GUILayout.BeginHorizontal();
            GUILayout.Label(config.name + " | " + config.GetInstanceID());

            if (GUILayout.Button(is_saved ? "UNSAVE" : "SAVE"))
            {
              
                if (!is_saved)
                {
                    config.CompleteInSave();
                } else
                {
                    config.RemoveFromSave();
                }

                GUILayout.EndHorizontal();
            }
            GUILayout.EndHorizontal();
        }
        
    }

}
