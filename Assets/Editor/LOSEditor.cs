using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LOS))]
public class LOSEditor : Editor
{

    LOS m_path;

    void OnEnable()
    {
        m_path = target as LOS;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Chk LOS")) m_path.Test() ;


    }




}
