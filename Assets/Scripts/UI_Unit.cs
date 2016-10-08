using UnityEngine;
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
        GameObject obj = (Instantiate(Resources.Load("unit_ui")) as GameObject);

        GameObject ui_parent = GameObject.FindGameObjectWithTag("UI_World");

        if (ui_parent != null)
        {
            obj.transform.SetParent(ui_parent.transform, false);
            obj.GetComponent<UI_Unit>().SetUnitInfo(u);
        }
    }
 
    void Update()
    {
        UpdatePosition();
    }

    Vector3 GetUnitToolTipPosition()
    {
        return m_unit.transform.position + Vector3.up * 0.5f;
    }
    public void UpdatePosition()
    {
        UI_WorldPos worldpos = GetComponent<UI_WorldPos>();

        if(m_unit != null)
             worldpos.SetWorldPosition(GetUnitToolTipPosition());
    }

    public void SetUnitInfo(Unit u)
    {
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
        Unit.OnUnitKilled += CheckKilled;

        m_unit.Actions.OnActionComplete += ActionComplete;
        UpdateAP();
        UpdateValues( );
    }

 
    void CheckKilled(Unit u)
    {
 
        if (u == m_unit)
        {
            Unit.OnUnitKilled -= CheckKilled;
           
            Unit.OnUnitHover -= CheckHovered;
            Unit.OnUnitHoverEnd -= CheckHoverEnd;

            Unit.OnTurnStart -= TurnStart;
            Unit.OnTurnEnded -= TurnEnd;

            u.Stats.OnStatUpdated -= UpdateValuesDelayed;

            if (gameObject.activeSelf)
            {
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
        yield return new WaitForSeconds(0.5f);
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
            Debug.Log("Update Values " + m_unit);
            Alphas.AddItem(ValuesNeedUpdateItem);
            StopAllCoroutines();
            StartCoroutine(IEUpdateValuesDelayed());
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
        if (u == m_unit)
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
        StatBar.SetBarValues(
            (int) m_unit.Stats.GetStatAmount(StatType.oxygen),
            (int) m_unit.Stats.GetStatAmount(StatType.adrenaline),
            (int) m_unit.Stats.GetStatAmount(StatType.vitality)
        );
       
    }
    IEnumerator IEUpdateValuesDelayed()
    {

        while( AlphaStackController.GetAlpha() < ValuesNeedUpdateItem.Alpha)
        {
            yield return null;
        }

        UpdateValues();

        yield return new WaitForSeconds(0.25f);

        Alphas.RemoveItem(ValuesNeedUpdateItem);
    }
}
