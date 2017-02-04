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

        UnitInventory inventory = unit.GetComponent<UnitInventory>();
        inventory.OnInventoryUpdated += SpawnInventoryNotification;

        PlayerInventory.Instance.OnInventoryUpdated += SpawnInventoryNotification;

        Unit.OnTurnStart += ShowActiveEffects;

        Unit.OnUnitKilled += u =>
        {
            if (u == unit)
            {
                RemoveListeners();
            };
        };

        Unit.OnEvacuated += u =>
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

        m_UnitUI.UnregisterEffectQeue();
    }
 

    void SpawnEffectExpiredNotification(UnitEffect effect)
    {
        if (!effect.ShowRemoveNotification ) return;

        EventNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect, effect.GetShortHandle()+" removed");
        EnableUnitUI();
    }
     
    void SpawnEffectTickNotification(UnitEffect effect)
    {

        EventNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect );
        EnableUnitUI();
    }
    void SpawnEffectAppliedNotification(UnitEffect effect)
    {
        if (!effect.ShowApplyNotification) return;
        EventNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect, effect.GetShortHandle()+" applied");
        EnableUnitUI();
    }

    void SpawnInventoryNotification(IInventoryItem item, int count)
    {       
        if (count <= 0 || !TurnSystem.HasTurn(m_Unit)) return;

        EventNotification.SpawnInventoryNotification(EffectNotifactionPrefab, EffectNotificationsContainer, item, count, "");
        EnableUnitUI();
    }

    void EnableUnitUI()
    {
        m_UnitUI.RegisterEffectQueue();

        StopAllCoroutines();
        StartCoroutine(HideWhenEmpty());
        gameObject.name = "TOGGLED";
    }
    void ShowActiveEffects(Unit unit)
    {
        if (unit != m_Unit) return;

        List<UnitEffect> effects = unit.GetComponent<Unit_EffectManager>().ActiveEffects;
        if (effects == null) return;

        foreach ( var effect in effects)
        {
            EventNotification.SpawnEffectNotification(EffectNotifactionPrefab, EffectNotificationsContainer, effect);
        }
    }

    void RemoveListeners()
    {
        m_Unit.GetComponent<Unit_EffectManager>().OnEffectTick -= SpawnEffectTickNotification;
        Unit.OnTurnStart -= ShowActiveEffects;

        m_EffectManager.OnEffectTick -= SpawnEffectTickNotification;
        m_EffectManager.OnEffectAdded -= SpawnEffectAppliedNotification;
        m_EffectManager.OnEffectRemoved -= SpawnEffectExpiredNotification;
        PlayerInventory.Instance.OnInventoryUpdated -= SpawnInventoryNotification;
    }


}
