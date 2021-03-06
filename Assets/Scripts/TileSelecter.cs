﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void TileEventHandler(Tile t);
public class TileSelecter : MonoBehaviour {

    public static TileEventHandler OnTileSelect;
    public static TileEventHandler OnTileHover;
    public static TileEventHandler OnTileUnhover;

    static TileSelecter _instance;
        
    public static Tile SelectedTile;
    public static Tile HoveredTile;
 

    void Awake()
    {
        _instance = this;

        Unit.OnUnitHover +=   OnUnitHover;
    }

   
    void OnDestroy()
    {
        Unit.OnUnitHover -= OnUnitHover;
        SelectedTile = null;
        HoveredTile = null;
    }
    public void OnUnitHover(Unit u)
    {
          HoverTile(u.currentTile);
    }
    public static void SelectTile(Tile t)
    { 
      // MDebug.Log("select tile");

        SelectedTile = t;
       // MDebug.Log("selected tile");
        if (OnTileSelect != null) OnTileSelect(t);
    }

    public static void HoverTile(Tile t)
    {
        HoveredTile = t;
        
        if (OnTileHover != null) OnTileHover(t);
    }

    public static void UnhoverTile(Tile t)
    {
        if(HoveredTile == t)
        {
            HoveredTile = null;
            if (OnTileUnhover != null)
                OnTileUnhover(t);
        }
    }
    
    public static void SetUnitColliders(bool b)
    {
        foreach(Unit u in Unit.AllUnits)
        {
            u.SetColliderState(b);

        }
    }
}
