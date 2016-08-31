using UnityEngine;
using System.Collections;

public class UI_ShowToolTip_TurnList : UI_ShowToolTip_Base {

    public GameObject CrumbleTooltip;
    public GameObject UnitTooltip;
    public GameObject TestTooltip;

    protected override void SpawnToolTip(object _obj)
    {
        ITurn item = (_obj as ITurn);

        if(item as Unit != null)
        {
            ToolTipInstance = Instantiate(UnitTooltip);
            return;
        }

        if(item as WorldCrumbler != null)
        {
            ToolTipInstance = Instantiate(CrumbleTooltip);
            return;
        }

        ToolTipInstance = Instantiate(TestTooltip);
    }

    protected override void InitializeToolTip(object _obj)
    {
        if( (_obj as WorldCrumbler == null) && (_obj as Unit == null))
        {
            _obj = new GenericToolTipTarget("GENERIC TOOLTIP");
        }
        base.InitializeToolTip(_obj);
    }

}
