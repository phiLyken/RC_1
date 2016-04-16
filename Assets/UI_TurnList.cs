using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_TurnList : MonoBehaviour {

    UI_TurnListItem[] turnlist_items;

    void UpdateList(List<ITurn> list)
    {
        for(int i = 0; i < turnlist_items.Length; i++)
        {
            turnlist_items[i].gameObject.SetActive(i < list.Count);
            if( i < list.Count)
            {
                turnlist_items[i].SetTurnItem(list[i]);
            }
        }
    }
    
    void Start()
    {
        turnlist_items = GetComponentsInChildren<UI_TurnListItem>();

        TurnSystem.Instance.OnListUpdated += UpdateList;
    }
}
