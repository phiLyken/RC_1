using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileMesh : MonoBehaviour {

    public List<TileMesh_Sub> SubMeshes;
    Tile m_tile;

    void OnParentCrumble(Tile t)
    {
        if (this == null || transform == null || t == null)
        {
            DestroyImmediate(this);
            return;
        }

        //we need to check whether the tile 
        //enabled to prevent it from reactivating it although it has crumbled
        if (!t.isEnabled) return;

        int current_crumble = t.CrumbleStage;

        for(int i = 0; i< SubMeshes.Count; i++)
        {
            TileMesh_Sub sub = SubMeshes[i];
            if(sub.CrumbleStage == current_crumble)
            {
                sub.EnableSub(TileManager.Instance, t);
            } else
            {
                sub.DisableSub();
            }
        }
    }

    public void UpdatePos(Tile t)
    {
        if (this == null) {
            DestroyImmediate(this);
            return;
        }
      
        transform.position = GetMeshPosition();
    }
    public void SetTile(Tile t)
    {
       
        m_tile = t;
        UpdatePos(m_tile);

        OnParentCrumble(t);
        t.OnTileCrumble += OnParentCrumble;
        t.OnTileRemoved += OnParentDeactivate;
        t.OnTileMove += UpdatePos;
    }

    Vector3 GetMeshPosition()
    {
        return  m_tile.GetPosition() + Vector3.up * -0.05f;
    }

    void OnParentDeactivate(Tile t)
    {
        SubMeshes.ForEach(m => m.DisableSub());
       
        if (this == null)
        {
            DestroyImmediate(this);
            return;
        }

       
    }



}
