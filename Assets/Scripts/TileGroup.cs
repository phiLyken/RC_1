using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileGroup {

    public List<Tile> Group;
    static int gr;

    public TileGroup(List<Tile> tiles)
    {
        gr++;

        Group = tiles;
      
        foreach(Tile t in Group)
        {
            t.OnTileCrumble += OnTileInGroupCrumble;
        }
    }

    void OnTileInGroupCrumble(Tile tile)
    {
        int _heightStepDelta = GetGetHeightDeltaForGroup(Group);

        if(_heightStepDelta != 0) { 
            Group.ForEach(t => t.MoveTileDown(_heightStepDelta));
        }
    }
    
    int GetGetHeightDeltaForGroup(List<Tile> tiles)
    {
        int count = tiles.Count;
        float diff = tiles.Sum(t => t.GetCrumbleToHeightDiff());

        return (int) Mathf.Abs( (diff / count) );     
    }

    bool CanTilesCrumble(List<Tile> tiles)
    {
        foreach(Tile t in tiles)
        {
            if (t.GetCrumbleToHeightDiff() >= 0) return false;
        }
        return true;
    }

    public static List<TileGroup> GetGroupsFromTiles(List<Tile> tiles)
    {
        IEnumerable< IGrouping<int, Tile>> groups =  tiles.GroupBy(t => t.TileGroup);

        List<TileGroup> aggretaged_groups = new List<TileGroup>();

        foreach( IGrouping<int, Tile> group in groups)
        {
            aggretaged_groups.Add(new TileGroup(group.ToList()));           
        }

        return aggretaged_groups;
    }

}
