using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public delegate void CrumbleEvent(int row);
public class WorldCrumbler : MonoBehaviour, ITurn {

    //Speed: Rows per turn the crumble progresses
    //Every Row < than the crumble row will get 1 crumbleturn
    public MyMath.R_Range TilesToCrumbleCount = new MyMath.R_Range(3, 5);
    public static  WorldCrumbler Instance;
    public CrumbleEvent OnCrumble;
    public int CrumbleTurnCost;
    public int TurnTime;
    int starting_order;
    bool hasCrumbled;

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

    public Color GetColor()
    {
        return Color.grey;
    }

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
		//Debug.Log("set turns: "+turns);
        TurnTime += turns;
    }

    public bool HasEndedTurn()
    {
        return hasCrumbled;
    }

    public void StartTurn()
    {
        hasCrumbled = false;
        ToastNotification.SetToastMessage1("Crumbling");  
        
        if(PanCamera.Instance != null)
        {
            PanCamera.Instance.PanToPos(GetCameraPosition(), CrumbleTurn);
        }      else
        {
            CrumbleTurn();
        }
    }

    Vector3 GetCameraPosition()
    {
        TilePos centertile = new TilePos( (int)TileManager.Instance.GridWidth / 2, 
            Mathf.Min(TileManager.Instance.GetLastActiveRow(), TileManager.Instance.GridHeight-1));

        return TileManager.Instance.Tiles[centertile.x, centertile.z].GetPosition();
    }

    void CrumbleTurn()
    {
        SetCrumbleInWeightedTiles();
        if (OnCrumble != null)
        {
            OnCrumble(0);
        }

        hasCrumbled = true;
    }
    void SetCrumbleInWeightedTiles()
    {
        int count = (int) TilesToCrumbleCount.Value();        
        TileWeighted.GetCrumbleTiles(count, TileManager.Instance).ForEach(t => t.StartCrumble() );
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


}
