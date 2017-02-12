using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RegionSelecterButton : UI_ButtonGetSet<RegionConfigDataBase>
{

    public Text TF;
    public GameObject Locked;
    public GameObject Selected;

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
         
        TF.text = item.Difficulty.ToString();
        Locked.SetActive(!item.IsUnlocked());
        SetUnselected();
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
