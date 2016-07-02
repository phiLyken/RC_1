using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void UnitEventHandler(Unit u);
public class Unit : MonoBehaviour, ITurn, IDamageable {

    public int TurnTime;
    int starting_order;
    public int OwnerID;

    public UnitStats Stats;
    public ActionManager Actions;

    public static List<Unit> AllUnits = new List<Unit>();
    public static Unit SelectedUnit;
    public static Unit HoveredUnit;
    public static UnitEventHandler OnUnitKilled;
    public static UnitEventHandler OnUnitHover;
    public static UnitEventHandler OnUnitHoverEnd;
    public static UnitEventHandler OnUnitSelect;

    public DamageEventHandler OnDamageReceived;
  
    public UnitEventHandler OnActionSelectedInUnit;

    public TurnableEventHandler OnUpdateTurnTime; 
    
    public UnitEventHandler OnTurnStart;
    public UnitEventHandler OnTurnEnded;
    
    public bool PrePlaced = true;

    [HideInInspector]
    public Tile currentTile;
        
    public bool CanBeActivated()
    {
        return  (OwnerID == 0 || ActiveRangeToPlayerUnits(this));
    }
    public bool IsActive
    {
        get {  return _isActive; } set { _isActive = value; }
    }
    bool _isActive;
    bool _isDead;


    void Awake()
    {
        Stats = GetComponent<UnitStats>();
        Stats.OnHPDepleted += KillUnit;

        Actions = GetComponent<ActionManager>();
        Actions.SetOwner(this);
        Actions.OnActionSelected += ActionChanged;
        Actions.OnActionUnselected += ActionChanged;
     //   TurnSystem.Instance.OnGlobalTurn += GlobalTurn;

        AllUnits.Add(this);

        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
            b = gameObject.AddComponent<SelectibleObjectBase>();
       
        b.OnSelect += UnitSelected;
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;
              
        UI_Unit.CreateUnitUI(this);

        GameObject obj = (Instantiate(Resources.Load("selected_effect"))) as GameObject;
        obj.GetComponent<ToggleActiveOnTurn>().SetUnit(this);
        
        if(PrePlaced)
            SetTile(TileManager.Instance.GetClosestTile(transform.position), true);
    }

    void ActionChanged(UnitActionBase b)
    {
        if (OnUpdateTurnTime != null)
            OnUpdateTurnTime(this);
    }

    public bool IsDead()
    {
        return _isDead;
    }

    void ActivationCheck()
    {
        if (!IsActive && CanBeActivated())
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (_isDead) return;
        _isActive = true;
        RegisterTurn();
    }

    void Start()
    {
        ActivationCheck();
        TurnSystem.Instance.OnGlobalTurn += GlobalTurn;        
    }    

    public void OnHover()
    {
        if (OnUnitHover != null) OnUnitHover(this);
        HoveredUnit = this;    
    }

    void OnHoverEnd()
    {
        if (OnUnitHoverEnd != null) OnUnitHoverEnd(this);
    }

    public void SetTile(Tile t, bool updatePosition )
    {
        if (currentTile != null)
        {
            currentTile.OnDeactivate -= OnCurrentTileDeactivate;
            if(currentTile.Child == this.gameObject)
            {
                currentTile.Child = null;
            }
        }
        if(t != null) { 
            currentTile = t;
            currentTile.OnDeactivate += OnCurrentTileDeactivate;
            t.SetChild(this.gameObject);
        }

        if (updatePosition)
        {
            transform.position = t.GetPosition();
        }
    }
   
    void OnCurrentTileDeactivate(Tile t)
    { 
        if (t == currentTile) KillUnit();
    }


    void SetOwner(int playerID)
    {
        OwnerID = playerID;
    }  

   //Unselects the currently selected (static) unit
    void UnSelectCurrent()
    {
        if(SelectedUnit != null)
        {
            SelectedUnit.UnselectUnit();
            SelectedUnit = null;
        }
    }

    public void UnitSelected()
    {
        if(Input.GetKey(KeyCode.T))
         ReceiveDamage(new Effect_Damage());
        
        if (OnUnitSelect != null) OnUnitSelect(this);
        return;
    }        

    void UnselectUnit()
    {    }

    Player GetOwner()
    {
        return PlayerManager.GetPlayer(OwnerID)   ;
    }    

    public void KillUnit()
    {
        _isDead = true;
        if (OnUnitKilled != null) OnUnitKilled(this);

        StartCoroutine(RemoveWhenReady());
                     
    }
    IEnumerator RemoveWhenReady()
    {
        while (TurnEventQueue.Current != null) yield return null;
        new TurnEventQueue.CameraFocusEvent(currentTile.GetPosition(), RemoveUnitFromGame);
    }
    public void RemoveUnitFromGame()
    {
        //TODO: CLEAN UP THIS DEPENCY MESS
        currentTile.OnDeactivate -= OnCurrentTileDeactivate;
        TurnSystem.Instance.OnGlobalTurn -= GlobalTurn;

        if(GetOwner() !=null)
            GetOwner().RemoveUnit(this);

        TurnSystem.Unregister(this);
        AllUnits.Remove(this);
        Destroy(this.gameObject);
    }

    public int GetCurrentTurnCost()
    {
       // if (!TurnSystem.HasTurn(this)) return 0;

        int turncost = 0;
        var _currentAction = Actions.GetCurrentAction();
       // Debug.Log(_currentAction);
        if (_currentAction != null)
        {
            turncost += _currentAction.TurnTimeCost;
           // Debug.Log(_currentAction.ActionID+ " "+ _currentAction.TurnTimeCost);
        }
        int cost = turncost + Actions.CurrentTurnCost;
       // Debug.Log("Current turn cost " + gameObject.name + " :" + cost);
        return cost;
    }

    public int GetTurnTime()
    {
        //Debug.Log("ID "+GetID()+" "+time+ "  "+time);
       // if (TurnSystem.HasTurn(this)) return 0;
        return TurnTime;
    }
   
    public void SetNextTurnTime(int turns)
    {
        if (_isDead) return;
       // Debug.Log(gameObject.name+" next turn time " + turns);
        TurnTime += turns;
    }

    public void EndTurn()
    {
        SetNextTurnTime(GetCurrentTurnCost());
        Actions.Reset();
       
       
        if (OnTurnEnded != null) OnTurnEnded(this);
    }

    public void StartTurn()
    {
        starting_order++;

        Debug.Log("NEW TURN:" + GetID()); 
        
        //TODO Add Listener for this 
        if(UI_ActiveUnit.Instance != null) {
            UI_ActiveUnit.Instance.SetActiveUnit(this);
        }
        UnSelectCurrent();

        //TODO ADD Listener for this
       if(PanCamera.Instance != null)
        PanCamera.Instance.PanToPos(currentTile.GetPosition());

        SelectedUnit = this;
        if (OnTurnStart != null) OnTurnStart(this);

    }

    public void GlobalTurn(int turn)
    {
        ActivationCheck();

        if (IsActive)
        {            
            TurnTime--;

            if (currentTile.isCamp)
                BaseCampTurn();            
        }

      //  Debug.Log(gameObject.name + " global turn new time:" + TurnTime);   
    }

    void BaseCampTurn()
    {
        Actions.RestCharges();

        PlayerUnitStats stats = Stats as PlayerUnitStats;

        if(stats != null)
        {
            stats.Int = 0;
            stats.Will = stats.Max;           
        }
    }

    public bool HasEndedTurn()
    {
        if (Actions.HasAP()) return false;
        if (Actions.IsActionInProgress) return false;

       // Debug.Log("turn ended ");

        //Debug.Break();
        return true;
    }    

    public void UnRegisterTurn()
    {
        TurnSystem.Unregister(this);
    }

    public string GetID()
    {
        return gameObject.name;
        return gameObject.name+" ["+(GetTurnTime()+GetCurrentTurnCost()).ToString()+"]";
    }

    public Color GetColor()
    {
        return OwnerID == 0 ? Color.blue : Color.red;
    }
    public int GetTurnControllerID()
    {
        return OwnerID;
    }

    public void ReceiveDamage(Effect_Damage dmg)
    {
        
        Stats.ReceiveDamage(dmg);
        if (OnDamageReceived != null) OnDamageReceived(dmg);     

    }   
    
    #region
    /// <summary>
    /// Returns a list of all the units of the requested owner, -1 will select all
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public static List<Unit> GetAllUnitsOfOwner(int owner, bool ignoreBaseCamp)
    {
        List<Unit> list = new List<Unit>();
        foreach(Unit u in AllUnits)
        {
            if ( (owner == -1 || u.OwnerID == owner) && (ignoreBaseCamp || !u.currentTile.isCamp)) list.Add(u);
        }

        return list;
    }

    /// <summary>
    /// Returns whether this unit is within a certain Z range to a players unit
    /// </summary>
    /// <param name="enemyUnit"></param>
    /// <returns></returns>
    static bool ActiveRangeToPlayerUnits(Unit enemyUnit)
    {
        Unit firstPlayer = TileManager.Instance.FindFirstUnit(0);
        if (firstPlayer == null) return true;

        int firstPlayerUnit = firstPlayer.currentTile.TilePos.z;
        int enemyPos = enemyUnit.currentTile.TilePos.z;
      //  Debug.Log(firstPlayerUnit+ " - "+enemyPos);
        return Mathf.Abs(firstPlayerUnit - enemyPos) <= 8;
    }

 

    public void SkipTurn()
    {
        Actions.SkipTurn();
    }     

    public TurnableEventHandler TurnTimeUpdated
    {
        get { return OnUpdateTurnTime; }
       set { OnUpdateTurnTime = value; }
    }

    #endregion


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
