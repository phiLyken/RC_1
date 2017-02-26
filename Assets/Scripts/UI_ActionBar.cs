using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class UI_ActionBar : MonoBehaviour {

    ActionManager m_manager;
    

    public GameObject ButtonPrefab;
    public List<UI_ActionBar_ButtonAnchor> Anchors;
    public Dictionary<ActionButtonID, UI_ActionBar_Button> CurrentButtons;

    public Image Evac_Button_Icon;
    static UI_ActionBar instance;

    void Awake()
    {
        instance = this;
        SpawnButtons();
        ActionManager.OnActionManagerActivated += SetActions;
      
    }
    void OnDestroy()
    {
        ActionManager.OnActionManagerActivated -= SetActions;
    }

    public static void SetActions(ActionManager manager)
    {
        instance._setActions(manager);
    }

    public void SkipTurn()
    {
        if(m_manager != null &&  m_manager.GetOwnerID() == 0)
        {
            m_manager.SkipTurn();
        }
    }

    public void Evacuate()
    {
        if (m_manager != null && m_manager.GetOwnerID() == 0)
        {
            m_manager.Evacuate();
        }
    }
    void _setActions(ActionManager manager)
    {
        UpdateButtons(manager);
    }

    void OnSelectAction(UnitActionBase action)
    {
       // SetBaseStatesInButtons();
    }

    void SetBaseStatesInButtons()
    {
        foreach (var button in CurrentButtons)
        {
            button.Value.SetBaseState();
        }

        Evac_Button_Icon.color = m_manager.GetOwner().currentTile.isCamp ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1);
    }

    void UnSelectAction(UnitActionBase action)
    {
        SetBaseStatesInButtons();
    }
 
    void UpdateButtons(ActionManager manager)
    {
        if(m_manager != null && manager != m_manager)
        {             
            UnSelectAction(null);
      
        }
  
        if (m_manager != null)
        {
            m_manager.OnActionSelected -= OnSelectAction;
            m_manager.OnActionUnselected -= UnSelectAction;
            m_manager.GetActionOfType<UnitAction_Move>().OnActionComplete += action => 
            m_manager = null;
        }

        m_manager = manager;
  
        m_manager.OnActionSelected += OnSelectAction;
        m_manager.OnActionUnselected += UnSelectAction;
      

       
        foreach ( var button in CurrentButtons)
        {
            UnitActionBase actionb = GetActionForButton(m_manager.Actions.ToList(), button.Key);
            if(actionb != null)
            {
                button.Value.gameObject.SetActive(true);
                button.Value.SetAction(actionb, m_manager);
            } else
            {
                button.Value.gameObject.SetActive(false);
            }
        }


        Evac_Button_Icon.color = manager.GetOwner().currentTile.isCamp ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1);


    }


    void SpawnButtons()
    {
        CurrentButtons = new Dictionary<ActionButtonID, UI_ActionBar_Button>();

        foreach (var anchor in Anchors)
        {
            UI_ActionBar_Button button = anchor.Spawn();
            button.Hotkey = anchor.HotKey;

            button.OnActionHovered += OnSelectAction;

            CurrentButtons.Add(anchor.ButtonID, button);
        }

    }

    UnitActionBase GetActionForButton(List<UnitActionBase> actions, ActionButtonID id)
    {

        return actions.Where(ac => ac.target_button == id).FirstOrDefault();
 
    }
 

}
