using UnityEngine;
using System.Collections;
using System;

public delegate void CrumbleEvent(int row);
public class WorldCrumbler : MonoBehaviour, ITurn {

    //Speed: Rows per turn the crumble progresses
    //Every Row < than the crumble row will get 1 crumbleturn
    public int CrumbleSpeed;
    public int CrumblingRows;
    public int RandomCrumbleOffset;
    public float CrumbleChance;
    public static  WorldCrumbler Instance;
    public CrumbleEvent OnCrumble;
   
    float currentCrumbleLine;
    int currentCrumbleRow
    {
        get { return (int)currentCrumbleLine; }
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

    int TurnTime;

    public int GetTurnTimeCost()
    {
        return 10;
    }

    public int GetNextTurnTime()
    {
        return TurnTime;
    }

    public void SetNextTurnTime(int turns)
    {
        TurnTime = turns;
    }

    public bool HasEndedTurn()
    {
        return true;
    }

    public void StartTurn()
    {
        Debug.Log("Crumble Turn");
        currentCrumbleLine += CrumbleSpeed;
        if (OnCrumble != null)
        {
            OnCrumble(currentCrumbleRow);
        }
    }

    public void GlobalTurn()
    {
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
}
