using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(RegionConfig))]
[CanEditMultipleObjects]
public class RegionConfigEditor : Editor {

    RegionConfig m_config;
    void OnEnable()
    {
        m_config = target as RegionConfig;
    }


    TileManager m_manager;

    List<UnitSpawner> spawners;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



        if(GUILayout.Button("Check"))
        {
            m_manager = m_config.TileSet;
            
            spawners = m_manager.GetComponentsInChildren<UnitSpawner>().ToList();

        }

        if (m_config != null && spawners != null && spawners.Count > 0)
        {
            Dictionary<UnitSpawner, int> SpawnerCount = new Dictionary<UnitSpawner, int>();

            foreach (UnitSpawner s in spawners)
            {
                if (!SpawnerCount.ContainsKey(s))
                {
                    SpawnerCount.Add(s, 0);
                }
                SpawnerCount[s]++;
            }


            foreach (KeyValuePair<UnitSpawner, int> pair in SpawnerCount)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "SpawnerGroupdID: "+pair.Key.SpawnerGroupID.ToString(), "Count: "+pair.Value.ToString());
                EditorGUILayout.EndHorizontal();

            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("----- Check ----");
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < m_config.Groups.Count;i++) 
            {
               
                
                string result = "";
            

                if(spawners.Where(sp => sp.SpawnerGroupID == m_config.Groups[i].SpawnerGroup).ToList().Count == 0)
                {
                    result = " NO SPAWNER FOUND FOR " + m_config.Groups[i].SpawnerGroup;
                } else
                {
                     result =" Check - Spawner Found";
                }

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("Group " + i, result);
                EditorGUILayout.EndVertical();
            }

          
        }
    }
}
