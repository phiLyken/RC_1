using UnityEngine;
using System.Collections;


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

    
}
