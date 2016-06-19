using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorTileMeshContainer : MonoBehaviour {

    public Dictionary<Tile, TileMesh> map;
    static EditorTileMeshContainer container;

    public static void AddPair(Tile t, TileMesh m)
    {
        if(container == null)
        {
            container = new GameObject().AddComponent<EditorTileMeshContainer>();
            container.name = "Tile Mesh Container";           
        }

        if(container.map == null)
        {
            Reset();
        }

        if (container.map.ContainsKey(t))
        {
            TileMesh mesh = container.map[t];
            if(mesh != null)
            {
                DestroyImmediate(mesh.gameObject);
            }

            container.map[t] = m;
        } else
        {
            container.map.Add(t, m);
           
        }
        m.transform.SetParent(container.gameObject.transform);
    }

    public static void Reset()
    {
        EditorTileMeshContainer container = FindObjectOfType<EditorTileMeshContainer>();

        if (container == null)
        {
            return;
        }

        container.map = new Dictionary<Tile, TileMesh>();
        MyMath.DeleteChildren(container.gameObject);
    }
    void Awake()
    {
        map = null;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
