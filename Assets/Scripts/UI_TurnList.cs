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
    Dictionary<UI_TurnListItem, int> positions;
    Dictionary<RectTransform,Sequence> move_sequences;

    public TurnSystem m_turnsystem;

    public RectTransform parent_container;

    List<RectTransform> anchors;

    public GameObject TurnListAnchorPrefab;
    public UI_TurnListItem TurnListItemPrefab;
    
    public void Init(TurnSystem turn_system)
    {
        CreateAnchors();
        views = new ViewList<ITurn, UI_TurnListItem>();
        positions = new Dictionary<UI_TurnListItem, int>();
        move_sequences = new Dictionary<RectTransform, Sequence>();
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
        UI_TurnListItem new_item = Instantiate(TurnListItemPrefab.gameObject).GetComponent<UI_TurnListItem>();
        new_item.transform.SetParent(this.transform,false); 
        new_item.SetTurnItem(u);

        return new_item;
    }

    void CreateAnchors()
    {
        anchors = MyMath.SpawnObjectsToTransform<RectTransform>(TurnListAnchorPrefab, parent_container, TurnListEntries);
       
        Canvas.ForceUpdateCanvases();
    }

    void ViewsUpdated(Dictionary<ITurn, UI_TurnListItem> list)
    {
        int i = 0;
        foreach(var pair in list)
        {
            UpdateSlotPosition(pair.Key, pair.Value, i);           
            i++;
        }
    }

    void UpdateSlotPosition(ITurn turn, UI_TurnListItem view, int position_in_list)
    {

        if (position_in_list >= TurnListEntries)
        {
            view.gameObject.SetActive(false);
            return;
        }

        view.gameObject.SetActive(true);

        int from_slot = 0;

        if (!positions.ContainsKey(view))
        {
            positions.Add(view, position_in_list);
        }
        else
        {
            from_slot = positions[view];
            positions[view] = position_in_list;
        }

         MoveView(view, position_in_list, from_slot);
        
    }

    void MoveView(UI_TurnListItem view, int slot_id_to, int slot_id_from )
    {
        view.TF.text = slot_id_to.ToString();
        RectTransform target = view.GetComponent<RectTransform>();
        StopMove(target);
        if (  Math.Abs(slot_id_to - slot_id_from) > 1 )
        {
            LongSlotMove(target, slot_id_from, slot_id_to);
        } else
        {
            ShortSlotMove(target, slot_id_to);
        }
    }

    Vector3 GetTargetSlotPos(int pos)
    {
      //  Debug.Log(pos +" "+anchors.Count);
        return anchors[pos].position;
    }

    void StopMove(RectTransform view)
    {
        if (move_sequences.ContainsKey(view))
        {
            move_sequences[view].Kill();
            move_sequences.Remove(view);
        }
    }

    Sequence LongSlotMove(RectTransform view, int start_slot, int target_slot_id)
    {
        Vector3 first_pos = GetTargetSlotPos(start_slot) - new Vector3(0, 50,0);
        Vector3 second_pos = GetTargetSlotPos(target_slot_id) - new Vector3(0, 50, 0);
        Vector3 final_pos = GetTargetSlotPos(target_slot_id);
         
        Sequence move = DOTween.Sequence();
        
        move.Append(view.DOMove(first_pos, 0.5f));
        move.Append(view.DOMove(second_pos, 1f));
        move.Append(view.DOMove(final_pos, 0.5f));
        move.AppendCallback(() => move_sequences.Remove(view));

        move_sequences.Add(view, move);

        return null;
    }

    Sequence ShortSlotMove(RectTransform view, int slot_id)
    {     
        RectTransform rect = view.GetComponent<RectTransform>();
        Vector3 target_position = GetTargetSlotPos(slot_id);
        Sequence move = DOTween.Sequence();

        move.Append(rect.DOMove(target_position, 0.4f));
        move.AppendCallback(() => move_sequences.Remove(view));

        move_sequences.Add(view, move);

        return move;
    }
}
