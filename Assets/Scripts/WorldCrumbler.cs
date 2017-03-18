using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public delegate void CrumbleEvent(int row);
public class WorldCrumbler : MonoBehaviour, ITurn {

    //Speed: Rows per turn the crumble progresses
    //Every Row < than the crumble row will get 1 crumbleturn
    public M_Math.R_Range TilesToCrumbleCount = new M_Math.R_Range(3, 5);
    public static  WorldCrumbler Instance;
    public CrumbleEvent OnCrumble;
    public int CrumbleTurnCost;
    public float TurnTime;
    public Sprite CrumbleSprite;
    int starting_order;
    protected bool hasCrumbled;

    public bool IsActive
    {
        get{return true; } set { }
    }

    public virtual void Init(TurnSystem system)
    {
 
        Instance = this;
        ActivateTurnSystem();
    }
    
    protected virtual void ActivateTurnSystem()
    {
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
    public float GetTurnTime()
    {
       
        return TurnTime;
    }

    public void SetNextTurnTime(float turns)
    {
		//MDebug.Log("set turns: "+turns);
        TurnTime += turns;
    }

    public bool HasEndedTurn()
    {
        return hasCrumbled;
    }

    protected  virtual string GetCrumbleText()
    {
        return "The earth is shaking...";
    }
    virtual public void StartTurn()
    {
        hasCrumbled = false;

        ToastNotification.SetToastMessage1(GetCrumbleText());  
        
        if(RC_Camera.Instance != null)
        {
            RC_Camera.Instance.ActionPanToPos.GoToPos(GetCameraPosition(), CrumbleTurn);
        }    else
        {
            CrumbleTurn();
        }
    }

    protected Vector3 GetCameraPosition()
    {
        TilePos centertile = new TilePos( (int)TileManager.Instance.GridWidth / 2, 
            Mathf.Min(TileManager.Instance.GetLastActiveRow(), TileManager.Instance.GridHeight-1));

        return TileManager.Instance.GetTileClamped(centertile.x, centertile.z).GetPosition();
    }

    protected void CrumbleTurn()
    {
        MDebug.Log("Crumble");
        SetCrumbleInWeightedTiles();

        if (OnCrumble != null)
        {
            OnCrumble(0);
        }

        if (Application.isPlaying)
        {
            StartCoroutine(WaitForMovingTiles());
        } else
        {
            hasCrumbled = true;
        }

     
    }

    bool TilesMoving()
    {
        foreach ( Tile t in TileManager.Instance.Tiles)
        {
            if (t.IsMoving)
                return true;
        }

        return false;
    }

    IEnumerator WaitForMovingTiles()
    {

        while (TilesMoving())
        {
           // MDebug.Log("waiting,..");
            yield return new WaitForSeconds(0.1f);
        }

        PostMoveTiles();

    }
    protected virtual void PostMoveTiles()
    {
        hasCrumbled = true;
    }
    protected virtual int GetCrumbleCount()
    {
        return (int) TilesToCrumbleCount.Value();
    }
    void SetCrumbleInWeightedTiles()
    {
      
     
      //  MDebug.Log("count " + count);

       // MDebug.Log("crumble. last row " + TileManager.Instance.GetLastActiveRow());    
        TileWeighted.GetCrumbleTiles(GetCrumbleCount(), TileManager.Instance).ForEach(t => t.StartCrumble() );
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
        return "SEISMIC ACTIVITY [" + TurnTime + "]" ;
    }

    public int GetTurnControllerID()
    {
        return -1;
    }

    public void SkipTurn()
    {
        throw new NotImplementedException();
    }

	public Action<ITurn> TurnTimeUpdated 
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

    public Sprite GetIcon()
    {
        return CrumbleSprite;
            }

    public List<List<Tile>> CrumbeGroups;

    public event System.Action OnUpdateSprite;
}
