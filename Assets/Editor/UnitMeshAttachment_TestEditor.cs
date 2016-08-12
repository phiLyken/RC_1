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

        if (GUILayout.Button("Attach Wepon&Armor to Player"))
        {
            if (m_test.armor_attachement == null)
            {
                m_test.armor_attachement = m_test.gameObject.AddComponent<UnitMesh_Attachment>();
            }
            m_test.armor_attachement.Init(armor_test_config.Parts, TargetUnitMesh);

            if (m_test.weapon_attachments == null)
            {
                m_test.weapon_attachments = m_test.gameObject.AddComponent<UnitMesh_Attachment>();

            }
            m_test.weapon_attachments.Init(weapon_test.Attachments, TargetUnitMesh);
        }
        if (GUILayout.Button("Attach Wepon&Armor to Player"))
        {
            if (m_test.test_attachment == null)
            {
                m_test.test_attachment = m_test.gameObject.AddComponent<UnitMesh_Attachment>();
            }

            m_test.test_attachment.Init(m_test.attachment_test, TargetUnitMesh);

        }
    }
}
