using UnityEngine;
using System.Collections;

public class TacticLoader : MonoBehaviour {

    public TurnSystem Turn;

    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("UI") == null) { 

            GameObject UI = Instantiate(Resources.Load("tactic_ui")) as GameObject;

            UI_TurnList turnlist = UI.GetComponentInChildren<UI_TurnList>();
            turnlist.Init(Turn);
        }
    }

}
