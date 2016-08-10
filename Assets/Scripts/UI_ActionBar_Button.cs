using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_ActionBar_Button : MonoBehaviour, IToolTip{

    UnitActionBase m_action;
    ActionManager m_manager;

    public Image ChargesCounterIMG;
    public Text ChargesCounterTF;

    public Image ActionIcon;
    public ActionEventHandler OnActionHovered;
     
    public void SetAction(UnitActionBase action, ActionManager manager)
    {
       // Debug.Log("set action " + action.ActionID);
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

    public UnitActionBase GetAction()
    {
        return m_action;
    }

    public void OnActionSelect(UnitActionBase action)
    {
      //  Debug.Log("Action select");
        ActionIcon.color = Color.cyan;
    }

    public void OnActionUnselect(UnitActionBase action)
    {
      //  Debug.Log("Action unselect");
        SetBaseState(action);
       
    }

    public void SetBaseState()
    {
        SetBaseState(m_action);
    }

    public void SetBaseState(UnitActionBase action)
    {
        ActionIcon.sprite = action.GetImage();
        ActionIcon.color = action.CanExecAction(false) ? Color.green : Color.red;
        UpdateChargers(action);
    }

    public void UpdateChargers(UnitActionBase action)
    {
        ChargesCounterIMG.gameObject.SetActive(action.ChargeController.useCharges);
        ChargesCounterTF.gameObject.SetActive(action.ChargeController.useCharges);

        ChargesCounterIMG.color = action.ChargeController.HasCharges() ? Color.white : Color.red;
        ChargesCounterTF.text = action.ChargeController.HasCharges().ToString() + "/" + action.ChargeController.GetMax();

        ChargesCounterTF.color = action.ChargeController.HasCharges() ? Color.black : Color.white;
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

    public object GetItem()
    {
        return GetAction();
    }
}
