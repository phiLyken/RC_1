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
        UI_Prompt.MakePrompt(UI_ActionBar_ButtonAnchor.GetAnchor(ActionButtonID.special_atk_1),"Use the more powerful \"Special Attack\" to kill the target", 2,
                           delegate { return !rush.HasRush;   },                        true);
        
    }

    void ShowStimpackPrompt()
    {
        UI_Prompt.MakePrompt(UI_ActionBar_ButtonAnchor.GetAnchor(ActionButtonID.stim), "Use stimpacks to regain Oxygen. Units without oxygen, die.", 2, delegate
        { return !CanShowStimpackPrompt(); }, true);
    }

    bool CanShowStimpackPrompt()
    {
        Debug.Log(m_Unit.Stats.GetStatAmount(StatType.oxygen) + " " + m_Unit.Inventory.GetItem(ItemTypes.rest_pack).GetCount());
        return m_Unit.Stats.GetStatAmount(StatType.oxygen) <= 3 && m_Unit.Inventory.HasItem(ItemTypes.rest_pack, 1);
    } 

    void UpdatedStat(Stat updated)
    {
      if(updated.StatType == StatType.oxygen && CanShowStimpackPrompt()){
            ShowStimpackPrompt();
        }
      
    }

    void UpdatedInventory(IInventoryItem item, int count)
    {
        if(item.GetItemType() == ItemTypes.rest_pack && count > 0 && CanShowStimpackPrompt())
        {
            ShowStimpackPrompt();
        }
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

                m_Unit.Stats.OnStatUpdated += UpdatedStat;
                m_Unit.Inventory.OnInventoryUpdated += UpdatedInventory;
            }
        }
    }
}

