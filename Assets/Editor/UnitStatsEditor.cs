using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(UnitStats))]
public class UnitStatsEditor : Editor {

    public override void OnInspectorGUI()
    {
        UnitStat_TestEditor.DrawStats(target as UnitStats);
    }
}
