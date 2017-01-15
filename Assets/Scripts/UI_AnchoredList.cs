using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using DG.Tweening;

public class UI_AnchoredList : MonoBehaviour {

    int PositionCount;

    public GameObject TurnListAnchorPrefab;
    Dictionary<RectTransform, int> positions;
    Dictionary<RectTransform, Sequence> move_sequences;
    List<RectTransform> anchors;
    public RectTransform parent_container;

    public void Init(int num)
    {
        PositionCount = num;
        CreateAnchors(num);
      
        positions = new Dictionary<RectTransform, int>();
        move_sequences = new Dictionary<RectTransform, Sequence>();
    }
    void CreateAnchors(int num)
    {
        anchors = M_Math.SpawnObjectsToTransform<RectTransform>(TurnListAnchorPrefab, parent_container, num);

        Canvas.ForceUpdateCanvases();
    }

    public  void UpdateSlotPosition(RectTransform view, int position_in_list)
    {

        if (position_in_list >= PositionCount)
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

    void MoveView(RectTransform view, int slot_id_to, int slot_id_from)
    {
        RectTransform target = view.GetComponent<RectTransform>();
        StopMove(target);
        if (Math.Abs(slot_id_to - slot_id_from) > 1)
        {
            LongSlotMove(target, slot_id_from, slot_id_to);
        }
        else
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
        Vector3 first_pos = GetTargetSlotPos(start_slot) - new Vector3(0, 25, 0);
        Vector3 second_pos = GetTargetSlotPos(target_slot_id) - new Vector3(0, 25, 0);
        Vector3 final_pos = GetTargetSlotPos(target_slot_id);

        Sequence move = DOTween.Sequence();

        move.Append(view.DOMove(first_pos, 0.05f));
        move.Append(view.DOMove(second_pos, 0.15f));
        move.Append(view.DOMove(final_pos, 0.05f));
        move.AppendCallback(() => move_sequences.Remove(view));

        move_sequences.Add(view, move);

        return null;
    }

    Sequence ShortSlotMove(RectTransform view, int slot_id)
    {
        RectTransform rect = view.GetComponent<RectTransform>();
        Vector3 target_position = GetTargetSlotPos(slot_id);
        Sequence move = DOTween.Sequence();

        move.Append(rect.DOMove(target_position, 0.1f));
        move.AppendCallback(() => move_sequences.Remove(view));

        move_sequences.Add(view, move);

        return move;
    }

}
