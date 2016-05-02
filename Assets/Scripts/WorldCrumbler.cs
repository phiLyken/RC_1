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
    public int CrumbleTurnCost;
    float currentCrumbleLine;
    int currentCrumbleRow
    {
        get { return (int)currentCrumbleLine; }
    }

    public bool IsActive
    {
        get{return true; } set { }
    }

    void Awake()
    {
        Instance = this;
        TurnSystem.Instance.OnGlobalTurn += GlobalTurn;
        RegisterTurn();
    }
    public void EndTurn()
    {
        SetNextTurnTime(CrumbleTurnCost);
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

    public Color GetColor()
    {
        return Color.grey;
    }
    public int TurnTime;

    public int GetCurrentTurnCost()
    {
        if (TurnSystem.HasTurn(this)) {
            return CrumbleTurnCost;
        }

        return 0;
    }
    public int GetTurnTime()
    {
       
        return TurnTime;
    }

    public void SetNextTurnTime(int turns)
    {
		Debug.Log("set turns: "+turns);
        TurnTime += turns;
    }

    public bool HasEndedTurn()
    {
        return true;
    }

    public void StartTurn()
    {
		Debug.Log("Crumble");
        ToastNotification.SetToastMessage1("Crumbling");
        currentCrumbleLine += CrumbleDistancePerTurn;
        if (OnCrumble != null)
        {
            OnCrumble(currentCrumbleRow);
        }
    }

    public void GlobalTurn(int turn)
    {

        TurnTime--;
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

	public TurnableEventHandler TurnTimeUpdated 
	{
		get { return null;}
		set{}
	}

	public int StartOrderID {
		get {
			return	starting_order;
		}
		set {
			starting_order = value;
		}
	}

	public void RegisterTurn()
	{
		starting_order =   TurnSystem.Register(this);
	}

	int starting_order;
}
