using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(BakeGrid))]
[CanEditMultipleObjects]
public class BakeGridEditor : Editor
{
    public static  Color[] col =
    {
        Color.red, Color.cyan, Color.yellow, Color.black, Color.green
    };

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Bake")) {

            List<List<Tile>> groups = (target as BakeGrid).Bake().Where(list => list.Count > 1).ToList();

            Debug.Log(groups.Count);
            for(int i = 0; i < groups.Count;i++)
            {
                foreach(Tile t in groups[i])
                {
                    Debug.DrawRay(t.transform.position, Vector3.up * 2, col[i %( col.Length-1)], 10f);
                }
            }

            SceneView.RepaintAll();
        }
    }
}