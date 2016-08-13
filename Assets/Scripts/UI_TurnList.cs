using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class UI_TurnList : MonoBehaviour {

    public TurnSystem m_turnsystem;

    public RectTransform parent_container;
    public int TurnListEntries;

    List<RectTransform> anchors;

    public GameObject TurnListAnchorPrefab;
    public UI_TurnListItem TurnListItemPrefab;
    Dictionary<ITurn, UI_TurnListItem> turnlist_items;

    public   void UpdateList(List<ITurn> list)
    {
        if (turnlist_items == null)
            return;

        List<ITurn> to_create = list.Where(item => !turnlist_items.ContainsKey(item)).ToList();

        to_create.ForEach(t => Debug.Log(t.GetID()));

        List<ITurn> items_to_delete = turnlist_items.Where(item => !list.Contains(item.Key)).Select(item => item.Key).ToList();

        items_to_delete.ForEach( item => {

            Destroy(turnlist_items[item].gameObject);
            turnlist_items.Remove(item);

        });

        Debug.Log(" to create: " + to_create.Count + "   to delete" + items_to_delete.Count);
        foreach(ITurn t in to_create)
        {

            turnlist_items.Add(t, MakeItem(t));
        }

        if(turnlist_items.Count != list.Count)
        {
            Debug.Log("WTF");
            return;
        }

        Dictionary<ITurn, UI_TurnListItem> new_list = new Dictionary<ITurn, UI_TurnListItem>();
        foreach(ITurn t in list)
        {
            new_list.Add(t, turnlist_items[t]);
        }
        turnlist_items = new Dictionary<ITurn, UI_TurnListItem>(new_list);
        SetItemsToPosition();
    }
    
    public void Init(TurnSystem turn_system)
    {

        CreateAnchors();
        turnlist_items = new Dictionary<ITurn, UI_TurnListItem>();

        if(turn_system != null)
        { 
            turn_system.OnListUpdated += UpdateList;
        }
    }
   
    void Start()
    {
       
       
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
    }

    void SetItemsToPosition()
    {
        int i = 0;
        foreach(var pair in turnlist_items)
        {
            Debug.Log(GetTargetSlotPos(i));
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
