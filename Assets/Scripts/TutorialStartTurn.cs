using UnityEngine;
using System.Collections;
using System;

class TutorialStartTurn : ITurn
{
    
    public bool IsActive
    {
        get
        {
            return true;
        }

        set
        {

        }
    }

    public int StartOrderID
    {
        get
        {
            return 0;
        }

        set
        {

        }
    }

    public Action<ITurn> TurnTimeUpdated
    {
        get
        {
            return null;
        }

        set
        {

        }
    }

    public event Action OnUpdateSprite;

    public void EndTurn()
    {
        UnRegisterTurn();
    }

    public Color GetColor()
    {
        return Color.magenta;
    }

    public int GetCurrentTurnCost()
    {
        return 1;
    }

    public Sprite GetIcon()
    {
        return null;
    }

    public string GetID()
    {
        return "TUTORIAL";
    }

    public int GetTurnControllerID()
    {
        return 1;
    }

    public float GetTurnTime()
    {
        return -1;
    }

    public void GlobalTurn(int turn)
    {

    }

    public bool HasEndedTurn()
    {
        return MissionSystem.HasCompletedGlobal("find_enemy");
    }

    public void RegisterTurn()
    {
        TurnSystem.Register(this);
    }

    public void SetNextTurnTime(float delta)
    {

    }

    public void SkipTurn()
    {

    }

    public void StartTurn()
    {

    }

    public void UnRegisterTurn()
    {
        TurnSystem.Unregister(this);
    }
}