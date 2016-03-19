using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CustomEditor(typeof(PathFindingTest))]
public class PathFindingTestEditor : Editor{

    PathFindingTest m_path;

    void OnEnable()
    {
        m_path = target as PathFindingTest;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Calc Path")) m_path.CalculatePath();

      
    }




}
