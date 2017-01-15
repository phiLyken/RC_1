using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;

public class UI_ActionBar_Button : MonoBehaviour, IToolTip{

    UI_AdrenalineRushBase Adr_Rush;
    UnitActionBase m_action;
    ActionManager m_manager;

    [HideInInspector]
    public KeyCode Hotkey;

    public Image ChargesCounterFill;
    public Image ChargesCounterBorder;
    public Image ChargesCounterIcon;

    public Text ChargesCounterTF;

    public Image ActionIcon;
    public Image ActionFrame;

    public float StatUpdateDelay;

    public ActionEventHandler OnActionHovered;

    UnitInventory inventory;

    public void SetAction(UnitActionBase action, ActionManager manager)
    {
        m_manager = manager;
        
        UnsetCurrentAction();
        SetNewAction(action);
    }
    void UnsetCurrentAction()
    {
        //   manager.OnActionComplete += OnActionComplete;
       
        if (m_action != null)
        {
            Debug.Log("^ui Button Unset Action " + m_action.ActionID);
            m_action.OnSelectAction -= OnActionSelect;
            m_action.OnUnselectAction -= OnActionUnselect;

 
            m_action.OnActionComplete -= OnActionComplete;
            m_action.GetOwner().Stats.OnStatUpdated -= OnStatUpdated;
            
            if (inventory != null)
                inventory.OnInventoryUpdated -= OnInventoryUpdate;

            m_action = null;

        }
    }

    void SetNewAction(UnitActionBase action)
    {
       
        m_action = action;

        if (m_action != null)
        {
            Debug.Log("^ui Button Set Action " + m_action.ActionID);
            //so we can test it also without owner
            if (action.GetOwner() != null)
            {
                inventory = action.GetOwner().Inventory;
                inventory.OnInventoryUpdated += OnInventoryUpdate;
                action.GetOwner().Stats.OnStatUpdated -= OnStatUpdated;

                if (Adr_Rush != null)
                {
                    Adr_Rush.Init(action.GetOwner().Stats);
                }

            }

            action.OnSelectAction += OnActionSelect;
            action.OnUnselectAction += OnActionUnselect;
            action.OnActionComplete += OnActionComplete;
            action.OnActionStart += OnActionComplete;

            action.GetOwner().Stats.OnStatUpdated += OnStatUpdated;
            SetBaseState(m_action);
        }
    }


    bool ShowAdrenalineRush()
    {
        return m_action.GetRequirements().Select(req => req.StatType).ToList().Contains(StatType.adrenaline);
    }
    void OnActionComplete(UnitActionBase _action)
    {
            SetBaseState();
   
    }
    void OnInventoryUpdate(IInventoryItem item, int count)
    {
        SetBaseState();
    }

    void OnStatUpdated( Stat s)
    {

        if (s.StatType == StatType.adrenaline && m_action.GetRequirements().Select(si => si.StatType).ToList().Contains(s.StatType))
        {
      
           M_Math.ExecuteDelayed(StatUpdateDelay,  SetBaseState);
        }
    }


    public UnitActionBase GetAction()
    {
        return m_action;
    }

    public void OnActionSelect(UnitActionBase action)
    {

        ApplyColorSetting(UI_ActionBar_Button_ColorSetting.GetInstance().BTN_Selected);
    }

    public void OnActionUnselect(UnitActionBase action)
    {
      //  Debug.Log("Action unselect "+action.ActionID);
        SetBaseState(action);
       
    }

    public void SetBaseState()
    {
        SetBaseState(m_action);
    }

    ColorSetting_Button GetBaseColorSetting(UnitActionBase action)
    {
        
        if(Adr_Rush != null && ShowAdrenalineRush() && Adr_Rush.HasRush  && action.CanExecAction(false) )
        {
            return UI_ActionBar_Button_ColorSetting.GetInstance().BTN_ADR_Rush;
        } else
        {
            return action.CanExecAction(false) ? UI_ActionBar_Button_ColorSetting.GetInstance().BTN_Active : UI_ActionBar_Button_ColorSetting.GetInstance().BTN_Inactive;
        }
    }

    public void SetBaseState(UnitActionBase action)
    {
        if (action == null)
            return;
       // Debug.Log("set base state " + action.ActionID);
        ActionIcon.sprite = action.GetImage();        
        
        ApplyColorSetting(GetBaseColorSetting(action));

        UpdateChargers(action);
    }

   
    public void UpdateChargers(UnitActionBase action)
    {
        ChargesCounterFill.gameObject.SetActive(action.ChargeController.useCharges);
        ChargesCounterTF.gameObject.SetActive(action.ChargeController.useCharges);

       
        ChargesCounterTF.text = action.ChargeController.GetChargesForType().ToString() + "/" + action.ChargeController.GetMax();

        
    }
    void ApplyColorSetting(ColorSetting_Button setting)
    {
        ChargesCounterFill.color = setting.Charge_Counter_Fill;
        ChargesCounterTF.color = setting.Charge_Text;
        ChargesCounterIcon.color = setting.Charge_CounterIcon;
        ChargesCounterBorder.color = setting.Charge_Counter_Frame;

        ActionIcon.color = setting.Icon;
        ActionFrame.color = setting.Frame;
    }
    public void SelectAction()
    {
        if(m_manager != null && m_manager.GetOwnerID() == 0)
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

    void Update()
    {
        if (Input.GetKeyDown(Hotkey))
        {
            SelectAction();
        }
    }

   void Awake()
    {
        Adr_Rush = GetComponent<UI_AdrenalineRushBase>();
        if (Adr_Rush != null)
        {
           
            Adr_Rush.OnRushGain += SetBaseState;
            Adr_Rush.OnRushFade += SetBaseState;
        }

    }


}
