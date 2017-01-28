using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using DG.Tweening;
public class UI_TurnList : MonoBehaviour {

    public int TurnListEntries;
    ViewList<ITurn, UI_TurnListItem> views;
 
    public RectTransform parent_container;    
    public UI_TurnListItem TurnListItemPrefab;
    public UI_AnchoredList AnchoredList;

    public void Init(TurnSystem turn_system)
    {
        AnchoredList.Init(TurnListEntries);

        views = new ViewList<ITurn, UI_TurnListItem>();

        views.Init(MakeItem, getTarget, RemoveView, 99);
              
        if(turn_system != null)
        {
            turn_system.OnListUpdated += OnListUpdate;
            OnListUpdate(turn_system.Turnables);
        }
    }
 
    public void OnListUpdate(List<ITurn> items)
    {
        ViewsUpdated(views.UpdateList(items));
    }

    UI_TurnListItem MakeItem(ITurn u, Transform target)
    {     
        UI_TurnListItem new_item = Instantiate(TurnListItemPrefab.gameObject).GetComponent<UI_TurnListItem>();
        new_item.transform.SetParent(target, false); 
        new_item.SetTurnItem(u);

        return new_item;
    }

    Transform getTarget(ITurn view)
    {
        return this.transform;
    }

    void RemoveView(UI_TurnListItem view)
    {
        view.Remove();
    }

    void ViewsUpdated(Dictionary<ITurn, UI_TurnListItem> list)
    {
        int i = 0;
        foreach(var pair in list)
        {
            RectTransform tr = pair.Value.GetComponent<RectTransform>();
            AnchoredList.UpdateSlotPosition(tr, i);           
            i++;
        }
    }

  
}
