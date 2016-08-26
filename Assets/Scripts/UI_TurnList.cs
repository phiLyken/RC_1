using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UI_TurnList : MonoBehaviour {

    public int TurnListEntries;

    ViewList<ITurn, UI_TurnListItem> views;

    public TurnSystem m_turnsystem;

    public RectTransform parent_container;

    List<RectTransform> anchors;

    public GameObject TurnListAnchorPrefab;
    public UI_TurnListItem TurnListItemPrefab;
    
    public void Init(TurnSystem turn_system)
    {
        CreateAnchors();
        views = new ViewList<ITurn, UI_TurnListItem>();
        views.Init(MakeItem);
        
        if(turn_system != null)
        {
            turn_system.OnListUpdated += OnListUpdate;
        }
    }
    
    public void OnListUpdate(List<ITurn> items)
    {
        ViewsUpdated(views.UpdateList(items));
    }
    UI_TurnListItem MakeItem(ITurn u)
    {
        Debug.Log(u.GetColor());
        UI_TurnListItem new_item = Instantiate(TurnListItemPrefab.gameObject).GetComponent<UI_TurnListItem>();
        new_item.transform.SetParent(this.transform,false); 
        new_item.SetTurnItem(u);

        return new_item;
    }

    void CreateAnchors()
    {
        anchors = MyMath.SpawnObjectsToTransform<RectTransform>(TurnListAnchorPrefab, parent_container, TurnListEntries);
        Debug.Log("ANCHORS " + anchors.Count);
        Canvas.ForceUpdateCanvases();
    }

    void ViewsUpdated(Dictionary<ITurn, UI_TurnListItem> list)
    {
        int i = 0;
        foreach(var pair in list)
        {
            if(i >= TurnListEntries)
            {
                pair.Value.gameObject.SetActive(false);
                continue;
            }
            Debug.Log(GetTargetSlotPos(i));

            pair.Value.gameObject.SetActive(true);
            pair.Value.GetComponent<RectTransform>().position = GetTargetSlotPos(i);

            i++;
        }

    }

    Vector3 GetTargetSlotPos(int pos)
    {
        Debug.Log(pos +" "+anchors.Count);
        return anchors[pos].position;
    }
}
