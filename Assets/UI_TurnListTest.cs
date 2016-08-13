using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_TurnListTest : MonoBehaviour {

    public TurnSystemMockData MockData;
    public UI_TurnList TurnList;
    List<ITurn> turnables;


    void Start()
    {
        turnables = MockData.GetMockTurnList();
        TurnList.Init(null);
        TurnList.UpdateList(turnables);
    }
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Fire1"))
        {
            turnables.Shuffle();
            TurnList.UpdateList(turnables);
        }
	}
}
