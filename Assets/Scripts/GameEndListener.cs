using UnityEngine;
using System.Collections;

public class GameEndListener : MonoBehaviour {

    public GameObject popupcontentprefabb;

	// Use this for initialization
	void Start () {
        Unit.OnUnitKilled += CheckUnitsLeft;
        Unit.OnEvacuated += CheckUnitsLeft;
	}
	
    void CheckUnitsLeft(Unit u)
    {
        int playerUnitsLeft = Unit.GetAllUnitsOfOwner(0, true).Count;
       
        if(playerUnitsLeft == 0)
        {
            UI_Popup_Global.ShowContent(popupcontentprefabb, false);
        }
    }
    void OnDestroy()
    {
        Unit.OnUnitKilled -= CheckUnitsLeft;
        Unit.OnEvacuated -= CheckUnitsLeft;
    }
 
}
