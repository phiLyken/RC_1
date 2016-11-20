using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(UnitStats_Test))]
[CanEditMultipleObjects]
public class UnitStat_TestEditor : Editor {

    UnitStats_Test m_test;

    public override void OnInspectorGUI()
    {
        //  base.OnInspectorGUI();

        base.OnInspectorGUI();
        m_test = target as UnitStats_Test;

        
        if(GUILayout.Button("Set Stats"))
        {
            m_test.MakeStats(m_test.baseStats);
        }
        if (m_test.m_stats == null)
            return;

            DrawStats(m_test.gameObject.GetComponent<UnitStats>());

    }

    public static void DrawStats(UnitStats stats)
    {

        List<StatType> all_types = System.Enum.GetValues(typeof(StatType)).Cast<StatType>().ToList();

        foreach(StatType t in all_types)
        {
             
            GUILayout.Label(t.ToString() + ": " + stats.GetStatAmount(t));
        }
    }

}
