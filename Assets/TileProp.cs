using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileProperties
{
    BlockWalkable,
    BlockSight
}
public class TileProp : MonoBehaviour {
    public List<TileProperties> Tags;
    
    void OnEnable()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }
}
