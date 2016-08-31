using UnityEngine;
using System.Collections;

public class AbilityToolTipTest : MonoBehaviour {

    public UnitActionBase Action;
    public UI_ActionBar_Button Button;

    void Start()
    {
        Button.SetAction(Action, null);
    }
}
