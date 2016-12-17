using UnityEngine;
using System.Collections;

public class TurnListToolTipTest : MonoBehaviour {

    public TurnSystemMockData.TurnSystemMock Mock;

    public  WorldCrumbler Crumble;


    public UI_TurnListItem item1;
    public UI_TurnListItem item2;
    // Use this for initialization
    void Start () {

        item1.SetTurnItem(Mock);

        item2.SetTurnItem(Crumble);
	}
	
 
}
