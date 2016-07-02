using UnityEngine;
using System.Collections;

public class TacticLoader : MonoBehaviour {

    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("UI") == null) { 
            Instantiate(Resources.Load("tactic_ui"));
        }
    }

}
