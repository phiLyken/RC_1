using UnityEngine;
using System.Collections;

public class MissionSystem_View : ObjectiveController_View {

	 
    void Awake()
    {
        MissionSystem.OnInit += SetItem;
    }


    void SetItem()
    {
        SetItem(MissionSystem.Instance);
    }
    void OnDisable()
    {
        MissionSystem.OnInit -= SetItem;
    }
}
