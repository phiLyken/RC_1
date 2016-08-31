using UnityEngine;
using System.Collections;

public class BindTurnSystemUI : MonoBehaviour {

    public TurnSystem TurnSystem;
    public UI_TurnList TurnList;

	// Use this for initialization
	void Start () {
        TurnList.Init(TurnSystem);
	}
	
 
}
