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

    UnitInventory inventory;
      
    public void SetAction(UnitActionBase action, ActionManager manager)
    {

        m_manager = manager;
        //   manager.OnActionComplete += OnActionComplete;
        // Debug.Log("set action " + action.ActionID);
        if (m_action != null)
        {
            action.OnSelectAction -= OnActionSelect;
            action.OnUnselectAction -= OnActionUnselect;
            inventory.OnInventoryUpdated -= OnInventoryUpdate;

        }
        
        

        m_action = action;

        if (m_action != null)
        {
            inventory = action.GetOwner().Inventory;
            inventory.OnInventoryUpdated += OnInventoryUpdate;
            action.OnSelectAction += OnActionSelect;
            action.OnUnselectAction += OnActionUnselect;
            action.OnActionComplete += OnActionComplete;
            SetBaseState(m_action);
        }
    }

   
    void OnActionComplete(UnitActionBase _action)
    {

            SetBaseState();
   
    }
    void OnInventoryUpdate(IInventoryItem item, int count)
    {
        SetBaseState();
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
         Debug.Log("Action unselect");
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
        ChargesCounterTF.text = action.ChargeController.GetChargesForType().ToString() + "/" + action.ChargeController.GetMax();

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
