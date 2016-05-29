using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RaysFromTiles : MonoBehaviour {


    public LayerMask mask;

    void OnDrawGizmos()
    {
        return;
        TileManager grid = GetComponent<TileManager>();

        foreach(Tile t in grid.FetchTiles())
        {


            Color c = IsTileblocked(t) ? Color.red : Color.cyan;
            foreach (var ray in GetRaysForTile(t)) {               
                Debug.DrawRay(ray.origin, ray.direction*200, c,0.1f);
            }
        }
    }

    public List<GameObject> GetPropsForTile(Tile t)
    {
        return MyMath.GetObjectsFromRays(   GetRaysForTile(t), "prop")
            .Select(_t => _t.collider.gameObject).GroupBy(prop => prop).Select(grp => grp.First()).ToList();
    }
    bool IsTileblocked(Tile t)
    {
        return MyMath.GetObjectsFromRays(GetRaysForTile(t), "prop").Count > 0;
    }
    List<Ray> GetRaysForTile(Tile t)
    {
        List<Ray> ret = new List<Ray>();
        foreach(var pos in GetRayPositionsFromTile(t))
        {
            
            
            ret.Add(new Ray(new Vector3(pos.x,-4,pos.z), Vector3.up * 4));
        }
        ret.Add(new Ray( new Vector3( t.transform.position.x, -4, t.transform.position.z), Vector3.up));
        
        return ret;
    }

    public Vector3[] GetRayPositionsFromTile(Tile t)
    {
        return MyMath.GetTransformBoundPositionTop(t.transform);
    }
    
}
