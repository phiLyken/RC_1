using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(CrumbleEdgeBorderManager))]
public class CrumbleEdgeBorderManagerEditor : Editor {

    void OnEnable()
    {
        
          //  (target as CrumbleEdgeBorderManager).SpawnEdges();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn Edges"))
                (target as CrumbleEdgeBorderManager).SpawnEdges();

    }
}
