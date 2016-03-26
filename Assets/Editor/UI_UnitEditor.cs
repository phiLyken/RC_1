using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_Unit))]
public class UI_UnitEditor : Editor
{
    UI_Unit m_UI_Unit;
    Unit selected;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_UI_Unit = (target as UI_Unit);
        selected = (Unit) EditorGUILayout.ObjectField(selected, typeof(Unit), true);


        if (selected!= null && GUILayout.Button("Set Unit"))
        {
            m_UI_Unit.SetUnitInfo(selected);
        }
    }
    void OnEnable()
    {

    }
}

