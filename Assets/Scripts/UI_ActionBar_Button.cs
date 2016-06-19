using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UI_ActionBar_Button : MonoBehaviour {

    UnitActionBase m_action;
    ActionManager m_manager;

    public Image ActionIcon;
    public ActionEventHandler OnActionHovered;
     
    public void SetAction(UnitActionBase action, ActionManager manager)
    {
        Debug.Log("set action " + action.ActionID);
        if(m_action != null)
        {
            action.OnSelectAction -= OnActionSelect;
            action.OnUnselectAction -= OnActionUnselect;
         
        }

        m_action = action;
        m_manager = manager;

        if (m_action != null)
        {
            action.OnSelectAction += OnActionSelect;
            action.OnUnselectAction += OnActionUnselect;
           
            SetBaseState(m_action);
        }

       
    }

    public void OnActionSelect(UnitActionBase action)
    {
        ActionIcon.color = Color.cyan;
    }

    public void OnActionUnselect(UnitActionBase action)
    {
        SetBaseState(action);
       
    }

    public void SetBaseState(UnitActionBase action)
    {
        ActionIcon.sprite = action.Image;
        ActionIcon.color = action.CanExecAction(false) ? Color.green : Color.red;
    }
    public void SelectAction()
    {
        if(m_manager.GetOwnerID() == 0)
            m_manager.SelectAbility(m_action);
    }

    public void MouseOver()
    {
        if (OnActionHovered != null) OnActionHovered(m_action);
    }

    public void MouseOverEnd()
    {
        if (OnActionHovered != null) OnActionHovered(null);
    }
}
