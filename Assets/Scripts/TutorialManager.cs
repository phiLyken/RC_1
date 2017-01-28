using UnityEngine;
using System.Collections;
using System;

public class TutorialManager : MonoBehaviour {

    TutorialStartTurn startTurn;

    void Awake()
    {
        startTurn = new TutorialStartTurn();
        startTurn.RegisterTurn();

        GameObject.FindObjectOfType<UI_TurnList>().gameObject.SetUIAlpha(0);
        GameObject.FindObjectOfType<UI_ActiveUnit>().gameObject.SetUIAlpha(0);

        _patrol_chance = Constants.AI_PATROL_CHANCE;

        MissionSystem.OnCompleteMission += OnObjectiveComplete;
    }

    float _patrol_chance;

    void OnObjectiveComplete(Objective complete)
    {

        switch (complete.GetSaveID())
        {
            case "find_enemy":
                Constants.AI_PATROL_CHANCE = 1;
                break;

            default:
                Constants.AI_PATROL_CHANCE = _patrol_chance;
                break;

        }
       
    }
}

