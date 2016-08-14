using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UnitMeshAttachment_Test))]
public class UnitMeshAttachment_TestEditor : Editor {

    public GameObject TargetUnitMesh;

    ArmorConfig armor_test_config;
    Weapon weapon_test;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
     
        UnitMeshAttachment_Test m_test = target as UnitMeshAttachment_Test;
        TargetUnitMesh = m_test.gameObject;

        armor_test_config = EditorGUILayout.ObjectField("Test Armor", armor_test_config, typeof(ArmorConfig), false) as ArmorConfig;
        weapon_test = EditorGUILayout.ObjectField("Test Armor", weapon_test, typeof(Weapon), false) as Weapon;

    }
}
