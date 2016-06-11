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

        transform.position = t.GetPosition() + Vector3.up * -0.05f;
    }
    public void SetTile(Tile t)
    {
        m_tile = t;
        SubMeshes = GetComponentsInChildren<TileMesh_Sub>().ToList();

        OnParentCrumble(t);
        t.OnTileCrumble += OnParentCrumble;
        t.OnDeactivate += OnParentDeactivate;
        t.OnTileMove += UpdatePos;
    }

    void OnParentDeactivate(Tile t)
    {
        if(this == null)
        {
            DestroyImmediate(this);
            return;
        }
        SubMeshes.ForEach(m => m.DisableSub());
    }



}
