using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_UnitTierLock_TierView : MonoBehaviour {

    public List<GameObject> Ranks;
    public List<GameObject> Unlocks;
    public LayoutElement Layout;
    public int TierHeight;

    public void SetTier(int tier)
    {

        Layout.minHeight += tier * TierHeight;

        for(int i = 0; i < Ranks.Count;i++)
        {
            bool active = i == tier;
         //   Debug.Log(Ranks[i].gameObject.name + " " + active);
            Ranks[i].SetActive(active);
         
        }

        for (int i = 0; i < Unlocks.Count; i++)
        {
            Unlocks[i].SetActive( i >= tier);
           
        }
    }


    public void Init(List<Unlockable<UnitTier>> tiers)
    {
        SetTier(tiers.GetUnlockedCount());
        for (int i = 0; i < Unlocks.Count; i++)
        {
            Unlocks[i].GetComponentInChildren<Text>().text = "Level "+ tiers[i].Item.LevelRequirement.ToString();
        }
    }
}

