using UnityEngine;
using System.Collections;

public class UI_PlayerLevel : UI_Level {

    void Start()
    {
        Init(PlayerLevel.Instance);
    }
	    
}
