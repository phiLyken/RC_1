using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathDisplay : MonoBehaviour {

    List<Vector3> points;
    public GameObject PointPrefab;
    public GameObject[] testObjects;
    LineRenderer Line;

    public static PathDisplay MakePathDisplay()
    {
        GameObject prefab = Resources.Load("path_display") as GameObject;
        GameObject go = Instantiate(prefab);
        return go.GetComponent<PathDisplay>();
    }
   

    void Start()
    {
        List<Vector3> l = new List<Vector3>();
        foreach(GameObject go in testObjects)
        {
            l.Add(go.transform.position);
        }

        //UpdatePositions(l);
    }

    List<Vector3> TilesToVectorList(List<Tile> tiles)
    {
        List<Vector3> m_list = new List<Vector3>();
        foreach (Tile tile in tiles) m_list.Add(tile.GetPosition()+ Vector3.up * 0.2f);
        return (m_list);
    }

    public void UpdatePositions(List<Tile> tlist)
    {
        UpdatePositions(TilesToVectorList(tlist));
    }

    public void UpdatePositions(List<Vector3> v3list)
    {
        //MDebug.Log(v3list.Count);
        points = v3list;
        if (Line == null) Line = GetComponent<LineRenderer>();

        gameObject.DeleteChildren();
       

        Line.SetVertexCount(v3list.Count);
        for (int i = 0; i < v3list.Count; i++)
        {
            (Instantiate(PointPrefab, v3list[i], Quaternion.identity) as GameObject).transform.parent = transform;
            Line.SetPosition(i, v3list[i]);
        }
    }
}
