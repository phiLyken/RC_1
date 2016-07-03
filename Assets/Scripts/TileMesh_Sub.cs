﻿using UnityEngine;
using System.Collections;

public class TileMesh_Sub : MonoBehaviour {

    public int CrumbleStage;
    public GameObject Sides;
    public GameObject Top;

    public void EnableSub(TileManager manager, Tile pos)
    {
       
        
        Sides.SetActive(false);
        gameObject.SetActive(true);
        foreach (Tile t in manager.GetTilesInRange(pos, 1))
        {
            if (t.currentHeightStep < pos.currentHeightStep)
            {
                Sides.SetActive(true);
                break;
            }          
        }

       // Debug.Log("enable submesh");
    }
    
    public void DisableSub()
    {
       // Debug.Log("disable sub mesh");
        gameObject.SetActive(false);
    }
}