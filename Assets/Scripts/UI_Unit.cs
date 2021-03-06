﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

 
public class UI_Unit : MonoBehaviour
{
    Unit m_unit;

    public Counter MoveField;
    public UnitBar StatBar;
    public UI_AlphaStackController AlphaStackController;
  

    AlphaStack Alphas;
    AlphaStack.AlphaStackItem HoverItem = new AlphaStack.AlphaStackItem(1.0f, "hover");
    AlphaStack.AlphaStackItem EffectQueueActiveItem = new AlphaStack.AlphaStackItem(1.0f, "effect queue");
    AlphaStack.AlphaStackItem ValuesNeedUpdateItem = new AlphaStack.AlphaStackItem(1.0f, "values need update turn");
    AlphaStack.AlphaStackItem ActiveTurnItem = new AlphaStack.AlphaStackItem(0.5f, "active turn");
    AlphaStack.AlphaStackItem DefaultItem = new AlphaStack.AlphaStackItem(0.0f, "default");

    public static void CreateUnitUI(Unit u)
    {
        GameObject obj = (Instantiate(Resources.Load("UI/unit_ui")) as GameObject);

        GameObject ui_parent = GameObject.FindGameObjectWithTag("UI_World");

        if (ui_parent != null)
        {
            obj.transform.SetParent(ui_parent.transform, false);
            obj.GetComponent<UI_Unit>().SetUnitInfo(u);
        }
    }
 
    void Update()
    {
       if(transform.parent != null)
         UpdatePosition();
    }

    Vector3 GetUnitToolTipPosition()
    {
        
        Debug.DrawLine(m_unit.transform.position, m_unit.transform.position + Vector3.up * 3f);
        return m_unit.transform.position + Vector3.up * 1.5f;
    }

    private Vector3 last_ui_pos;

    public void UpdatePosition()
    {
        UI_WorldPos worldpos = GetComponent<UI_WorldPos>();
       
        if(m_unit != null)
        {
            last_ui_pos = GetUnitToolTipPosition();
          
        }

        worldpos.SetWorldPosition(last_ui_pos);
    }

    public void SetUnitInfo(Unit u)
    {
        GetComponent<UI_AdrenalineRushBase>().Init(u.Stats);

        GameObject Unit_speech = Instantiate(Resources.Load("UI/unit_ui_speech") as GameObject);
        Unit_speech.transform.SetParent(transform.parent, false);
        Unit_speech.GetComponent<UI_ShowUnitSpeech>().Init(u);
        Alphas = new AlphaStack();
        AlphaStackController.Init(Alphas);
        Alphas.AddItem(DefaultItem);

        GetComponent<UI_EffectQueue>().SetUnit(u, this);

        m_unit = u;

        u.Stats.OnStatUpdated += UpdateValuesDelayed;

        Unit.OnTurnStart += TurnStart;
        Unit.OnTurnEnded += TurnEnd;

        Unit.OnUnitHover += CheckHovered;
        Unit.OnUnitHoverEnd += CheckHoverEnd;
        Unit.OnUnitKilled += CheckRemoved;
        Unit.OnEvacuated += CheckRemoved;

        m_unit.Actions.OnActionComplete += ActionComplete;
        UpdateAP();
        UpdateValues( );
    }

 
    void CheckRemoved(Unit u)
    {
        
        if (u == m_unit)
        {
            Unit.OnEvacuated -= CheckRemoved;
            Unit.OnUnitKilled -= CheckRemoved;
           
            Unit.OnUnitHover -= CheckHovered;
            Unit.OnUnitHoverEnd -= CheckHoverEnd;

            Unit.OnTurnStart -= TurnStart;
            Unit.OnTurnEnded -= TurnEnd;

            u.Stats.OnStatUpdated -= UpdateValuesDelayed;

            if (gameObject.activeSelf && u.IsDead())
            {
               
                StopAllCoroutines();
                StartCoroutine(DestroyDelayed());
            }
            else
            {
                    
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator DestroyDelayed()
    {

        yield return new WaitForSeconds(2.5f);
        
        Destroy(this.gameObject);
    } 

    void CheckHoverEnd(Unit _hovered)
    {
        if (_hovered == m_unit)
            Alphas.RemoveItem(HoverItem);
    }

    void CheckHovered(Unit _hovered)
    {
        if (_hovered == m_unit)
            Alphas.AddItem(HoverItem);
    }

    public void UnregisterEffectQeue( )
    {
        Alphas.RemoveItem(EffectQueueActiveItem);
    }

   public void RegisterEffectQueue()
    {
        Alphas.AddItem(EffectQueueActiveItem);
    }

    void UpdateValuesDelayed(Stat stat)
    {
        if( stat.StatType == StatType.vitality ||
            stat.StatType == StatType.oxygen ||
            stat.StatType == StatType.adrenaline)
        { 
            // MDebug.Log("Update Values " + m_unit+ " stat:"+stat.StatType.ToString());
            Alphas.AddItem(ValuesNeedUpdateItem);

            StopCoroutine("IEUpdateValuesDelayed");
            StartCoroutine("IEUpdateValuesDelayed");
        }
    }


    void ActionComplete(UnitActionBase action)
    {
        if (m_unit.IsDead())
            return;

        UpdateAP();
    }

    void TurnStart(Unit u)
    {
        if (u == m_unit && u.IsIdentified)
        { 
            Alphas.AddItem(ActiveTurnItem);        
            UpdateValues();
            UpdateAP();
        }
    }

    void TurnEnd(Unit u)
    {
        if(u == m_unit)
            Alphas.RemoveItem(ActiveTurnItem);
    }
    void UpdateAP()
    {
        MoveField.SetNumber(m_unit.Actions.GetAPLeft());
    }


    void UpdateValues()
    {

      //  MDebug.Log("update values " + m_unit.GetID());
        StatBar.SetBarValues(
            (int) m_unit.Stats.GetStatAmount(StatType.oxygen),
            (int) m_unit.Stats.GetStatAmount(StatType.adrenaline),
            (int) m_unit.Stats.GetStatAmount(StatType.vitality),
             m_unit.OwnerID
        );
       
    }
    IEnumerator IEUpdateValuesDelayed()
    {

        while( AlphaStackController.GetAlpha() < ValuesNeedUpdateItem.Alpha)
        {
           // MDebug.Log("Waiting for ALPHA " + ValuesNeedUpdateItem.Alpha);
            yield return null;
        }

        UpdateValues();

        yield return new WaitForSeconds(0.25f);

        Alphas.RemoveItem(ValuesNeedUpdateItem);
    }
}
