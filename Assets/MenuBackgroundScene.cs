using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuBackgroundScene : MonoBehaviour {
    public List<Tile> To_Crumble;
    public TileManager Tileset;

    void Start()
    {


        To_Crumble.ForEach(tile => tile.StartCrumble());
        To_Crumble.ForEach(tile => tile.StartCrumble());
        Tileset.GetTileList().ForEach(t => t.OnCrumbleTurn(0));
    }

}
