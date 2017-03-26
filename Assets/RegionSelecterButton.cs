using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RegionSelecterButton : UI_ButtonGetSet<RegionConfigDataBase>
{

    public Text TF;
    public GameObject Locked;
    public GameObject Selected;
    public GameObject Saved;

    public Image DifficultyImage;


    public override void Remove()
    {
        
    }

    public override void Updated()
    {
     //   MDebug.Log("set item");
    }
    
    protected override void OnSet(RegionConfigDataBase item)
    {
        //    MDebug.Log("set item");
        GetComponent<LayoutElement>().preferredWidth += item.AllPools.Count * 5;
        TF.text = item.SelectionName;
        Locked.SetActive(!item.IsUnlocked());
        SetUnselected();
        DifficultyImage.sprite = item.DifficultySprite;
        Saved.SetActive(item.IsCompleteInSave());
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
