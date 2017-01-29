using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class TutorialManager : MonoBehaviour {

    public GameObject ADR_Tutorial_PopupContent;
    public GameObject Intro_PopupContent;
    
    TutorialStartTurn startTurn;
    Unit m_Unit;
    UI_AdrenalineRushBase rush;
    bool ADR_Prompt_Shown;

    void Awake()
    {
        startTurn = new TutorialStartTurn();
        startTurn.RegisterTurn();

        GameObject.FindObjectOfType<UI_TurnList>().gameObject.SetUIAlpha(0);
        GameObject.FindObjectOfType<UI_ActiveUnit>().gameObject.SetUIAlpha(0);

        _patrol_chance = Constants.AI_PATROL_CHANCE;

    
        Constants.AI_PATROL_CHANCE = 1;

        UI_Popup_Global.ShowContent(Intro_PopupContent, true);
        UI_Popup_Global.Instance.OnCloseDone += StartMission;
    }

    float _patrol_chance;

    void StartMission()
    {
        UI_Popup_Global.Instance.OnCloseDone -= StartMission;
        MissionSystem.Instance.Init();
        GameObject.FindObjectOfType<UI_TurnList>().gameObject.SetUIAlpha(MissionSystem.HasCompletedGlobal("kill_enemy_1") ? 1 : 0);
        MissionSystem.OnCompleteMission += OnObjectiveComplete;
        TurnSystem.Instance.Init();

        GameObject.FindObjectOfType<WorldExtender_Tutorial>().SetTutorialState();


    }
    void OnObjectiveComplete(Objective complete)
    {

        switch (complete.GetSaveID())
        {
            case "move_to":
                Constants.AI_PATROL_CHANCE = _patrol_chance;
                break;

            case "kill_enemy_1":
                GameObject.FindObjectOfType<UI_TurnList>().gameObject.SetUIAlpha(1);
                break; 
        }       
    }

    void OnRush()
    {
        if (!ADR_Prompt_Shown)
        {
            ADR_Prompt_Shown = true;
            UI_Popup_Global.ShowContent(ADR_Tutorial_PopupContent, true);
            UI_Popup_Global.Instance.OnCloseDone += ShowSpecialPrompt;
        }
    }

    void ShowSpecialPrompt()
    {
            UI_Prompt.MakePrompt(
                           FindObjectsOfType<UI_ActionBar_ButtonAnchor>().Where( btn => btn.ButtonID == ActionButtonID.special_atk_1).First().transform as RectTransform,
                           "Use the more powerful \"Special Attack\" to kill the target", 2,
                           delegate {
                               return !rush.HasRush;
                           },
                        true);
        
    }

    void Update()
    {
        if(m_Unit == null)
        {
            m_Unit = Unit.GetAllUnitsOfOwner(0, true).FirstOrDefault();
            if(m_Unit != null)
            {
                rush = m_Unit.gameObject.AddComponent<UI_AdrenalineRushBase>();
                rush.Init(m_Unit.Stats);
                rush.OnRushGain += OnRush;
                rush.EnableDelay = 3f;

            }
        }
    }
}

