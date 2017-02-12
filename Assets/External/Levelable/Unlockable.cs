using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Unlockable<T>  : IUnlockable
{
    public T Item;
    Func<T, string> getID;

    protected int unlock_at;
    protected Levels m_levels;

    event Action<T> OnUnlock;

    public int GetLevelRequirement()
    {
        return unlock_at;
    }

    public bool IsUnlocked()
    {
        return m_levels == null || (m_levels.GetCurrentLevel() >= unlock_at);
    }

    public void SetLevels(Levels levels)
    {
        if (levels == null)
            return;
        m_levels = levels;
        m_levels.OnLevelUp += CheckLevelUp;
        CheckLevelUp(levels.GetCurrentLevel());
    }

    void CheckLevelUp(int new_level)
    {
        if (m_levels != null && new_level >= unlock_at)
        {
            m_levels.OnLevelUp -= CheckLevelUp;
            OnUnlock.AttemptCall(Item);
        }
    }

    public void AddUnlockListener(Action<T> _ononlock)
    {

        OnUnlock += _ononlock;

        if (IsUnlocked())
        {
            _ononlock(Item);
        }
    }

    public void AddUnlockListener(Action _ononlock)
    {
        OnUnlock += item => _ononlock();

        if (IsUnlocked())
        {
            _ononlock();
        }
    }

    public string GetID()
    {
        return getID(Item);
    }

    public Unlockable(T _item, Levels levels, int requirement, Func<T, string> _getID)
    {
        getID = _getID;
        unlock_at = requirement;
        Item = _item;
        
        SetLevels(levels);
       
    }
}
