using UnityEngine;
using System.Collections;

public class AbilityToolTipTest : MonoBehaviour {

    public UI_ToolTip_AbilityBase ToolTipAbility;
    public UnitActionBase Action;
    public UI_ActionBar_Button Button;

    void Start()
    {
       // Button.SetAction(Action, null);
        ToolTipAbility.SetAbility(Action);
    }
}
