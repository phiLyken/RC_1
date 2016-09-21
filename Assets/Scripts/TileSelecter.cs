using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void TileEventHandler(Tile t);
public class TileSelecter : MonoBehaviour {

    public static TileEventHandler OnTileSelect;
    public static TileEventHandler OnTileHover;

    static TileSelecter _instance;
        
    public static Tile SelectedTile;
    public static Tile HoveredTile;

    public GameObject PositionMarker;
    
    void Awake()
    {
        _instance = this;
        
        Unit.OnUnitHover += u =>
        {
            HoverTile(u.currentTile);
        };
    }
    public static void EnablePositionMarker(bool enabled)
    {
        _instance.PositionMarker.SetActive(enabled);
        SetUnitColliders(!enabled);
    }
    public static void SetPositionMarker(Tile t)
    {
        if (_instance == null) return;
        if (_instance.PositionMarker != null) _instance.PositionMarker.transform.position = t.GetPosition();

       
    }
    public static void SelectTile(Tile t)
    { 
      // Debug.Log("select tile");

        SelectedTile = t;
       // Debug.Log("selected tile");
        if (OnTileSelect != null) OnTileSelect(t);
    }

    public static void HoverTile(Tile t)
    {
        HoveredTile = t;
        SetPositionMarker(t);
        if (OnTileHover != null) OnTileHover(t);
    }

    
    static void SetUnitColliders(bool b)
    {
        foreach(Unit u in Unit.AllUnits)
        {
            Collider c = u.GetComponent<Collider>();
            c.enabled = b;
        }
    }
}
