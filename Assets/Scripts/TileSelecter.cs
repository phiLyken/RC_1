using UnityEngine;
using System.Collections;


public delegate void TileEventHandler(Tile t);
public class TileSelecter : MonoBehaviour {

    public static TileEventHandler OnTileSelect;
    public static TileEventHandler OnTileHover;

    static TileSelecter _instance;
        
    public static Tile SelectedTile;
    public GameObject PositionMarker;
    
    void Awake()
    {
        _instance = this;
    }

    public static void SetPositionMarker(Tile t)
    {
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
        
        SetPositionMarker(t);
        if (OnTileHover != null) OnTileHover(t);
    }

    
}
