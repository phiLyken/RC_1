using UnityEngine;
using System.Collections;

public class MissionEndedPopupTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        new MissionOutcome(3, 2, 2000, 3, 3, 0.5f, 2, 3);
        UI_Popup_Global.ShowContent(Resources.Load("UI/ui_popupcontent_game_finished_evac") as GameObject, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
