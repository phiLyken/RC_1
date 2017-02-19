using UnityEngine;
using System.Collections;

public class UI_PlayerLevel : UI_Level {

    public bool InitOnEnable;

    void OnEnable()
    {
        if(InitOnEnable)
            Init(PlayerLevel.Instance);
    }
	    
}
