using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Unit : MonoBehaviour {
       
    public Text UnitName;
    public Counter MoveField;
    public UnitBar StatBar;
    

    Unit m_unit;
    bool hovered;

    public static void CreateUnitUI(Unit u)
    {
        GameObject obj = (Instantiate(Resources.Load("unit_ui")) as GameObject);

        GameObject ui_parent = GameObject.FindGameObjectWithTag("UI");

        if(ui_parent != null) { 
            obj.transform.SetParent(ui_parent.transform, false);
            obj.GetComponent<UI_Unit>().SetUnitInfo(u);
        }
    }
    public void Toggle(bool active)
    {
        //Either we turn it on (there is no limitation for that)
        //or when we turn it off, we need to check if the queue is active or if it is overed
        if (active || GetCanHideUI())
        {
            gameObject.SetActive(active);
        }
       
    }
    void Update()
    {
        UpdatePosition();
    }
    
    public void UpdatePosition()
    {
        UI_WorldPos worldpos = GetComponent<UI_WorldPos>();
        worldpos.SetWorldPosObject( m_unit != null ? m_unit.transform : null);
    }

    public void SetUnitInfo(Unit u)
    {
        GetComponent<UI_EffectQueue>().SetUnit(u,this) ;
        
        m_unit = u;
        u.Stats.OnStatUpdated += OnUpdateStat;      

        u.OnTurnStart += UpdateUI;
        u.OnTurnEnded += TurnEnd;

        Unit.OnUnitHover += CheckHovered;
        Unit.OnUnitHoverEnd += CheckHoverEnd;
        Unit.OnUnitKilled += CheckKilled;

        m_unit.Actions.OnActionComplete += ActionComplete;
        UpdateUI(u);
    }

    void ActionComplete(UnitActionBase action)
    {
        if (m_unit.IsDead()) return;
        UpdateUI(m_unit);
    }
    void TurnEnd(Unit u)
    {
        StartCoroutine(HideDelayed());
    }
    IEnumerator HideDelayed()
    {
        yield return new WaitForSeconds(0.85f);
        Toggle(false);
    }
    //TODO Potential perfomance problem
    void OnUpdateStat()
    {       
        UpdateUI(m_unit);
    }
    void CheckKilled(Unit u)
    {
        if(u == m_unit) {
         
            Unit.OnUnitKilled -= CheckKilled;
            m_unit.Stats.OnStatUpdated -= OnUpdateStat;
            Unit.OnUnitHover -= CheckHovered;
            Unit.OnUnitHoverEnd -= CheckHoverEnd;
            u.OnTurnEnded -= TurnEnd;

            if (gameObject.activeSelf) { 
                StartCoroutine(DestroyDelayed());
            } else
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    IEnumerator DestroyDelayed()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);

    }
    bool GetCanHideUI()
    {
        return !TurnSystem.HasTurn(m_unit) && !GetComponent<UI_EffectQueue>().GetQueueActive() && !hovered;
    }

    void CheckHoverEnd(Unit _hovered)
    {
        if (_hovered == m_unit)
        {
            hovered = false;
            Toggle(false);
        }

    }
    void CheckHovered(Unit _hovered)
    {
        if (_hovered == m_unit && m_unit.IsActive)
        {
            hovered = true;
            UpdateUI(m_unit);
            Toggle(true);
        }
    }
    void UpdateUI(Unit u)
    {

        PlayerUnitStats p_stats = u.Stats as PlayerUnitStats;
        if(p_stats != null)
        {

            StatBar.SetBarValues(p_stats.GetStatAmount(UnitStats.StatType.will),
                                  p_stats.GetStatAmount(UnitStats.StatType.intensity),
                                  p_stats.GetStatAmount(UnitStats.StatType.max)
                                  );
            
        } else
        {
            
            StatBar.SetBarValues(
                (int) u.Stats.GetStatAmount(UnitStats.StatType.HP), 0,
                (int) u.Stats.GetStatAmount(UnitStats.StatType.max)
             );
        }


        MoveField.SetNumber(u.Actions.GetAPLeft());

        if (TurnSystem.HasTurn(m_unit))
        {
            Toggle(true);
        }
        else
        {
            Toggle(false);
            return;
        }
    }


}
