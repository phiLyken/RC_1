using UnityEngine;
using System.Collections;

public class BindTurnSystemUI : MonoBehaviour {

    public TurnSystem TurnSystem;
 
    public WorldCrumbler WorldCrumbler;

	// Use this for initialization
	void Start () {
      
        WorldCrumbler.Init(TurnSystem);
	}
	
 
}
