using UnityEngine;
using System.Collections;
using System;

public delegate void CrumbleEvent(int row);
public class WorldCrumbler : MonoBehaviour, ITurn {

    //Speed: Rows per turn the crumble progresses
    //Every Row < than the crumble row will get 1 crumbleturn
    public int CrumbleDistancePerTurn;
    public int CrumblingRows;
    public int RandomCrumbleOffset;
    public float CrumbleChance;
    public static  WorldCrumbler Instance;
    public CrumbleEvent OnCrumble;
    public int CrumbleSpeed;
    float currentCrumbleLine;
    int currentCrumbleRow
    {
        get { return (int)currentCrumbleLine; }
    }

    public bool IsActive
    {
        get
        {
            return true;
        }
    }

    void Awake()
    {
        Instance = this;
        RegisterTurn();
    }


    void OnDrawGizmos()
    {
        if(TileManager.Instance != null)
        {
            Gizmos.color = new Color(0.5f, 0, 0, 0.5f);
            Gizmos.DrawCube(
                TileManager.Instance.GetTilePos2D(TileManager.Instance.GridWidth / 2, currentCrumbleRow),
                new Vector3(TileManager.Instance.GridWidth,1,1 )
                );
        }
    }

    public int TurnTime;

    public int GetTurnTimeCost()
    {
        return CrumbleSpeed;
    }

    public int GetNextTurnTime()
    {
        return TurnTime;
    }

    public void SetNextTurnTime(int turns)
    {
        Debug.Log("set adsadsd");
        TurnTime = turns;
    }

    public bool HasEndedTurn()
    {
        return true;
    }

    public void StartTurn()
    {
        Debug.Log("Crumble Turn");
        currentCrumbleLine += CrumbleDistancePerTurn;
        if (OnCrumble != null)
        {
            OnCrumble(currentCrumbleRow);
        }
    }

    public void GlobalTurn(int turn)
    {
        Debug.Log("Global turn in crumble " + TurnTime);
        TurnTime--;
    }

    public void RegisterTurn()
    {
        TurnSystem.Register(this);
    }

    public void UnRegisterTurn()
    {
        throw new NotImplementedException();
    }

    public string GetID()
    {
        return "World Crumble [" + TurnTime + "]" ;
    }

    public int GetTurnControllerID()
    {
        return -1;
    }

    public void SkipTurn()
    {
        throw new NotImplementedException();
    }
}
