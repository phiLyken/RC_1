using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileCollectionHighlight  {

    static  List<Tile> CurrentCollection;
    
    public static void SetHighlight(List<Tile> tiles, string visual_state)
    {
        DisableHighlight();
        CurrentCollection = tiles;

        foreach (Tile t in CurrentCollection) t.SetVisualState(visual_state);
    }

    public static void DisableHighlight()
    {
        if(CurrentCollection != null)
        {
            foreach (Tile t in CurrentCollection) t.SetVisualState("normal");
            CurrentCollection.Clear();
        }
    }
    
}
