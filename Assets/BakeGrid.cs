using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class BakeGrid : MonoBehaviour {
    


    public static List<List<Tile>> Bake(TileManager region)
    {
        Debug.Log("--- Baking ----");
        Dictionary<Tile, List<GameObject>> map = new Dictionary<Tile, List<GameObject>>();
       
        //Get all the objects for a tile and add it to the map

        foreach ( Tile tile in region.GetTileList())
        {  
            map.Add(tile, RaysFromTiles.GetPropsForTile(tile));

        }
        Debug.Log("Generated Map for " + map.Count + " tiles");
        ApplyTagsToTiles(map);
        region.InvalidateGroups();
        return  MakeGroups(map);

       

    }    

    static void ApplyTagsToTiles(Dictionary<Tile, List<GameObject>> map)
    {
        foreach (var pair in map)
        {
            pair.Value.ForEach(go => {
                if(go.GetComponent<TileProp>() != null) { 
                    go.GetComponent<TileProp>().Tags.ForEach(tag => pair.Key.SetTileProperties(tag));
                } else
                {
                    Debug.LogWarning("No Prop tags found on " + go.name);
                }
            });
        }
    }
    
    static List<List<Tile>> MakeGroups(Dictionary<Tile, List<GameObject>> tile_map)
    {
        Dictionary<GameObject, List<Tile>> tiles_for_prop = new Dictionary<GameObject, List<Tile>>();
              
        //Generate the prop map
        foreach(var pair in tile_map)
        {
            foreach(GameObject prop in pair.Value)
            {              
                {                    
                    if (prop != null && !tiles_for_prop.ContainsKey(prop))
                        tiles_for_prop.Add(prop, new List<Tile>()  );
                } 
           }
        }

        Debug.Log(" Props Identified: " + tiles_for_prop.Count);

        //fill prop map (props now know what tiles they occupy)
        foreach(var pair in tile_map)
        {
            pair.Value.ForEach(prop => {
                if (!tiles_for_prop[prop].Contains(pair.Key)){
                    tiles_for_prop[prop].Add(pair.Key);
                    Debug.Log("Added tile to a prop" + prop.name);
                }
            });
        }

        List<Tile> tiles_without_group = tile_map.Keys.ToList();
        Debug.Log(" .." + tiles_without_group.Count);
        List<Tile> tiles_assigned = new List<Tile>();

        List<List<Tile>> groups = new List<List<Tile>>();

        int current_group_id = 0;

        foreach(Tile t in tiles_without_group)
        {
           
            if (!tiles_assigned.Contains(t))
            {
                current_group_id++;

                List<Tile> group = new List<Tile>();
                group.Add(t);

                List<GameObject> props_checked = new List<GameObject>();
                List<GameObject> new_props = GetNewPropsInGroup(group, tile_map, props_checked);
                
                while(new_props.Count > 0) { 
                    foreach (GameObject prop in  new_props )
                    {
                        props_checked.Add(prop);

                        group.AddRange(tiles_for_prop[prop].Where( _proptile => !group.Contains(_proptile)).ToList());
                    }

                    new_props = GetNewPropsInGroup(group, tile_map, props_checked);
                }

                //all the props are parented to the first tile
                if(props_checked.Count > 0)
                {
                    props_checked.ForEach(p => p.transform.SetParent(group[0].transform, true));
                }
                tiles_assigned.AddRange(group);
                group.ForEach(_group_tile => _group_tile.TileGroup = current_group_id);
                groups.Add(group);
            }
        }

       
        return groups;
    }
    
    static List<GameObject> GetNewPropsInGroup(List<Tile> tiles, Dictionary<Tile, List<GameObject>> tile_map, List<GameObject> obj)
    {
        List<GameObject> newprops = new List<GameObject>();
        foreach(Tile t in tiles)
        {
            newprops.AddRange(tile_map[t].Where(go => !obj.Contains(go)).ToList());
        }
        return newprops;
    }
}
