using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadSelecter : MonoBehaviour {

    public Transform Target;

    ViewList<Unlockable<TieredUnit>, UnitSelecterButton> selector_buttons;
   

    void Awake()
    {
        selector_buttons = new ViewList<Unlockable<TieredUnit>, UnitSelecterButton>().Init(MakeView, delegate
        { return Target; }, SquadManager.Instance.GetSelectible(), OnRemove, 20);
        SquadManager.Instance.ClearSelected();
    }
    

    void OnRemove(UnitSelecterButton t)
    {

    }

    void OnSelect(Unlockable<TieredUnit> tier)
    {

        Debug.Log("SQUAD SELECTER SELECT TIER "+ tier.Item.Tiers[0].Config.ID);
        if (SquadManager.Instance.selected_units.Contains(tier.Item))
        {
            selector_buttons.GetView(tier).SetUnselected();
            SquadManager.RemoveFromSquad(tier.Item);
        } else if( SquadManager.AddToSquad(tier))
        {
            selector_buttons.GetView(tier).SetSelected();
        }
    }

    public UnitSelecterButton MakeView(Unlockable<TieredUnit> unit_config, Transform target)
    {
        GameObject obj = Instantiate(Resources.Load("UI/ui_unit_selection_view")) as GameObject;
        obj.transform.SetParent(target, false);
    //    obj.transform.localScale = Vector3.one;
        UnitSelecterButton selector = obj.GetComponent<UnitSelecterButton>();
        selector.SetItem(unit_config, OnSelect);

        return selector;
    }
}
