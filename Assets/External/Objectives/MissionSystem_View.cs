using UnityEngine;
using System.Collections;

public class MissionSystem_View : ObjectiveController_View {

	 
    void Start()
    {
        MissionSystem.OnInit += () =>
        {
            Set(MissionSystem.Instance);
        };
    }
}
