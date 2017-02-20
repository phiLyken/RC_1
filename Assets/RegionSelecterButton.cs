using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RegionSelecterButton : UI_ButtonGetSet<RegionConfigDataBase>
{

    public Text TF;
    public GameObject Locked;
    public GameObject Selected;
    public Counter DifficultyCounter;

    public override void Remove()
    {
        
    }

    public override void Updated()
    {
     //   Debug.Log("set item");
    }
    
    protected override void OnSet(RegionConfigDataBase item)
    {
        //    Debug.Log("set item");
        GetComponent<LayoutElement>().preferredWidth += item.AllPools.Count * 5;
        TF.text = item.SelectionName;
        Locked.SetActive(!item.IsUnlocked());
        SetUnselected();
        DifficultyCounter.SetNumber(item.Difficulty+1);
    }

    public void SetSelected()
    {
        Selected.SetActive(true);
    }

    public void SetUnselected()
    {
        Selected.SetActive(false);
    }
}
