using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_EffectQueue : MonoBehaviour {

    public GameObject EffectNotifactionPrefab;
    public Transform EffectNotificationsContainer;
    UI_Unit m_UnitUI;
    Unit m_Unit;
    Unit_EffectManager m_EffectManager;

    public bool GetQueueActive()
    {
        return EffectNotificationsContainer.transform.childCount > 0;
    }
   
    public void SetUnit(Unit unit, UI_Unit unitUI)
    {
        m_UnitUI = unitUI ;
        m_Unit = unit;
        m_EffectManager = m_Unit.GetComponent<Unit_EffectManager>();

        m_EffectManager.OnEffectTick += SpawnEffectTickNotification;
        m_EffectManager.OnEffectAdded += SpawnEffectAppliedNotification;
        m_EffectManager.OnEffectRemoved += SpawnEffectExpiredNotification;

        m_Unit.OnTurnStart += ShowActiveEffects;

        Unit.OnUnitKilled += u =>
        {
            if (u == unit)
            {
                RemoveListeners();
            };
        };               
    }

    IEnumerator HideWhenEmpty()
    {
        while (GetQueueActive()) yield return true;

        m_UnitUI.Toggle(false);
    }

    void TryHide()
    {
        m_UnitUI.Toggle(false);
    }

    void SpawnEffectExpiredNotification(UnitEffect effect)
    {
        if (!effect.ShowRemoveNotification) return;

        EffectNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect, effect.GetString()+" removed");
        EnableUnitUI();
    }
     
    void SpawnEffectTickNotification(UnitEffect effect)
    {
        EffectNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect );
        EnableUnitUI();
    }
    void SpawnEffectAppliedNotification(UnitEffect effect)
    {
        if (!effect.ShowApplyNotification) return;
        EffectNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect, effect.GetString()+" applied");
        EnableUnitUI();
    }

    void EnableUnitUI()
    {
        m_UnitUI.Toggle(true);

        StopAllCoroutines();
        StartCoroutine(HideWhenEmpty());
        gameObject.name = "TOGGLED";
    }
    void ShowActiveEffects(Unit unit)
    {
        List<UnitEffect> effects = unit.GetComponent<Unit_EffectManager>().ActiveEffects;
        if (effects == null) return;

        foreach ( var effect in effects)
        {
            EffectNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect);
        }
    }

    void RemoveListeners()
    {
        m_Unit.GetComponent<Unit_EffectManager>().OnEffectTick -= SpawnEffectTickNotification;
        m_Unit.OnTurnStart -= ShowActiveEffects;
    }


}
