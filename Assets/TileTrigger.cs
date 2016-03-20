using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileTrigger : MonoBehaviour {

    public int TriggerCount;
    public int maxTriggered;
    public UnitEventHandler OnUnitEnter;

    //when a units walks on this tile the effect is triggered
    public List<Tile> TriggerTiles;

    void Start()
    {

    }
    
   
    
}
