using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorTileMeshContainer : MonoBehaviour {

    public Dictionary<Tile, TileMesh> map;
    static EditorTileMeshContainer _instance;

    static EditorTileMeshContainer GetInstance()
    {
        if(_instance != null)
        {
            return _instance;
        }

        _instance = FindObjectOfType<EditorTileMeshContainer>();

        if (_instance != null)
        {
            return _instance;
        }

        _instance = new GameObject().AddComponent<EditorTileMeshContainer>();
        _instance.name = "Tile Mesh Container";

        return _instance;

    }
    public static void AddPair(Tile t, TileMesh m)
    {

        EditorTileMeshContainer container = GetInstance();

        if (container.map == null)
        {
            Reset(container);
        }

        if (_instance.map.ContainsKey(t))
        {
            TileMesh mesh = _instance.map[t];
            if(mesh != null)
            {
                Destroy(mesh.gameObject);
            }

            _instance.map[t] = m;
        } else
        {
            _instance.map.Add(t, m);
           
        }

        m.transform.SetParent(_instance.gameObject.transform);
    }

    public static void Reset()
    {
        Reset(GetInstance());
    }
    public static void Reset(EditorTileMeshContainer container)
    {       
        if (container == null)
        {   
            return;
        }
        container.map = new Dictionary<Tile, TileMesh>();
        container.gameObject.DeleteChildren();
        
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
