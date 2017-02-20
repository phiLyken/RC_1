using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class RegionSelecter : MonoBehaviour
{
    UniqueSelectionGroup<RegionConfigDataBase> selection;

    List<RegionConfigDataBase> Regions;
    public Transform Target;

    ViewList<RegionConfigDataBase, RegionSelecterButton> selecters;
       
    public void Awake()
    {
        Regions = new List<RegionConfigDataBase>(Resources.Load<ScriptableRegionDataBaseConfigs>("Regions/RegionConfigs/selectablemissions_defaultbalancing").RegionConfigs);
        selecters = new ViewList<RegionConfigDataBase, RegionSelecterButton>().Init(MakeView, delegate
        { return Target; }, Regions, RemoveView, Regions.Count);

        selection = new UniqueSelectionGroup<RegionConfigDataBase>().Init(selecters.GetItems(), OnUnSelectedSetView, OnSelectedSetView);
    }
 
    RegionSelecterButton MakeView(RegionConfigDataBase config, Transform target)
    {
        RegionSelecterButton obj =  Resources.Load("UI/ui_regionselectionbutton").Instantiate<RegionSelecterButton>(target, true);
  
        obj.transform.SetParent(target);
        obj.transform.localScale = Vector3.one;

        obj.SetItem(config, OnSelect);

        return obj;
    }

    void RemoveView(RegionSelecterButton Button) {

    }

    void OnUnSelectedSetView(RegionConfigDataBase unselected)
    {
        selecters.GetView(unselected).SetUnselected();
    }

    void OnSelectedSetView(RegionConfigDataBase selected)
    {
        selecters.GetView(selected).SetSelected();
    }

    void OnSelect(RegionConfigDataBase selected)
    {
        if (!selected.IsUnlocked())
        {
            return;
        }
        Debug.Log("SELECTED REGION " + selected.name);
        selection.Select(selected);
        GameManager.Instance.ChoosenRegionConfig = selected;
    }
}
