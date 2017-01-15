using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RaysFromTiles : MonoBehaviour {


    public static List<GameObject> GetPropsForTile(Tile t)
    {
        return M_Math.GetObjectsFromRays(   GetRaysForTile(t), "prop")
            .Select(_t => _t.collider.gameObject).GroupBy(prop => prop).Select(grp => grp.First()).ToList();
    }
    static bool  IsTileblocked(Tile t)
    {
        return M_Math.GetObjectsFromRays(GetRaysForTile(t), "prop").Count > 0;
    }
    static List<Ray> GetRaysForTile(Tile t)
    {
        List<Ray> ret = new List<Ray>();
        foreach(var pos in GetRayPositionsFromTile(t))
        {
                       
            ret.Add(new Ray(new Vector3(pos.x,-4,pos.z), Vector3.up * 4));
        }
        ret.Add(new Ray( new Vector3( t.transform.position.x, -4, t.transform.position.z), Vector3.up));
        
        return ret;
    }

    public static Vector3[] GetRayPositionsFromTile(Tile t)
    {
        return M_Math.GetTransformBoundPositionTop(t.transform);
    }
    
}
