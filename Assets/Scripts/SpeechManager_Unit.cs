using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SpeechManager_Unit : MonoBehaviour {

    public delegate void SpeechEvent(Unit unit, string[] lines, string arg);

    int selectCounter;
    UI_AdrenalineRushBase rush;

    UnitSpeechConfig Config;
    Unit m_Unit;

    public static SpeechEvent OnSpeech;

    public void ShowCrumbleSpeech()
    {
         AttemptTrigger(Config.Crumble);       
    }

    public void ShowFriendDiedSpeech()
    {
       
        AttemptTrigger(Config.FriendDie);
    }

    public void ShowFoeDiedSpeech()
    {
        AttemptTrigger(Config.FoeDie);
    }


    public void ShowTurnSpeech()
    {
        AttemptTrigger(Config.FoeDie);
    }
    public void Init(UnitSpeechConfig config, Unit _m_Unit)
    {
        Config = config;
        SetUnit(_m_Unit);
    }

    void SetUnit(Unit _m_Unit)
    {
        m_Unit = _m_Unit;

        Unit.OnUnitKilled += CheckKilled;
        Unit.OnUnitSelect += CheckSelected;
        Unit.OnTurnStart += CheckStart;
        m_Unit.OnIdentify += Identify;
        m_Unit.OnDamageReceived += DmgReceived;
        m_Unit.Actions.OnActionStarted += ActionStarted;
       
        m_Unit.GetComponent<Unit_EffectManager>().OnEffectAdded += ReceivedEffect;
        
        rush =  gameObject.AddComponent<UI_AdrenalineRushBase>();
        rush.Init(m_Unit.Stats);
        rush.EnableDelay = 3f;

        if(Config.GainAdrRush != null)
            rush.OnRushGain += () => { AttemptTrigger(Config.GainAdrRush); };

        if(TurnSystem.Instance != null)
        {
            TurnSystem.Instance.OnGlobalTurn += ResetCounter;
        }
    }

    
    void ResetCounter(int c)
    {
        selectCounter = 0;
    }
    
    void Identify(Unit triggerer)
    {
        if(triggerer != null)
            AttemptTrigger(Config.Identify);
    }
    void ActionStarted(UnitActionBase action)
    {
        if(Config.UseAbility != null)
            AttemptTrigger(Config.UseAbility, action.ActionID);
    }
    void DmgReceived(UnitEffect_Damage dmg)
    {
        if(dmg.GetDamage() < m_Unit.Stats.GetStatAmount(StatType.oxygen))
        { 

            SpeechTriggerConfig trigger = dmg.GetDamage() > 3 ? Config.ReceiveDamageBig : Config.ReceiveDamageSmall;

            if (trigger != null)
                AttemptTrigger(trigger);
        }
    }

    void ReceivedEffect(UnitEffect effect)
    {
        if(Config.ReceiveEffect != null)
            AttemptTrigger(Config.ReceiveEffect, effect.Unique_ID);
    }

    void CheckKilled( Unit u )
    {
        if(u == m_Unit)
        {
            if(Config.Died != null)
                AttemptTrigger(Config.Died);

            Unit.OnUnitKilled -= CheckKilled;
        } 
    }

    void CheckStart(Unit u)
    {
        if(u == m_Unit)
        {
            AttemptTrigger(Config.Turn);
        }
    }
    void CheckSelected(Unit u)
    {

        if (u == m_Unit)
        {

            selectCounter++;

            if(selectCounter == 4)
            {
                if(Config.Selected != null)
                    AttemptTrigger(Config.Selected);
               
            }
           
        }
    }
    static void TriggerSpeech(Unit u, string[] lines)
    {
        /// can be executed delayed so check if null

        Debug.Log(" Trigger "+u.GetID());
        if(u != null)
        {
            if (OnSpeech != null)
                OnSpeech(u, lines, "");
        }
    }

    static void TriggerSpeech(Unit u, string[] lines, string arg)
    {
        /// can be executed delayed so check if null

        Debug.Log(" Trigger " + u.GetID());
        if (u != null)
        {
            if (OnSpeech != null)
                OnSpeech(u,  lines, arg);
        }
    }

    public void AttemptTrigger( SpeechTriggerConfig trigger)
    {
        Debug.Log("asdsd");
        if (trigger != null && m_Unit.IsIdentified)
        {
            Debug.Log("asdsd");
            TriggerDelayed( trigger.GetSpeech(), trigger.Delay.Value());
        }

    }

    public void AttemptTrigger(SpeechTriggerConfigSimple trigger)
    {
        if(trigger != null && m_Unit.IsIdentified)
            TriggerDelayed(trigger.GetSpeech(),trigger.Delay.Value() );

    }

    public  void AttemptTrigger(SpeechTriggerConfigID trigger, string ID)
    {
        if (trigger != null && m_Unit.IsIdentified)
        { 
            Debug.Log("Attempt trigger speech: " + trigger.ToString());
            TriggerDelayed(trigger.GetSpeech(ID), 0.1f);
        }

    }

    void TriggerDelayed(Speech speech, float delay)
    {
        if (speech != null)
        {
            StartCoroutine(M_Math.ExecuteDelayed(delay, () => TriggerSpeech(m_Unit, speech.Lines)));
        } else
        {
            Debug.LogWarning("speech not found");
        }
    }
    

 
    
}
