using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void UnitEventHandler(Unit u);
public class Unit : MonoBehaviour, ITurn, IDamageable {

    int starting_order;
    public int OwnerID;

    public UnitStats Stats;
    public ActionManager Actions;

    [HideInInspector]
    public UnitInventory Inventory;

    public static List<Unit> AllUnits = new List<Unit>();
    public static Unit SelectedUnit;
   
    public static Unit HoveredUnit;
    public static UnitEventHandler OnUnitKilled;
    public static UnitEventHandler OnUnitHover;
    public static UnitEventHandler OnUnitHoverEnd;
    public static UnitEventHandler OnUnitSelect;
    public static UnitEventHandler OnTurnStart;
    public static UnitEventHandler OnTurnEnded;
        
    public DamageEventHandler OnDamageReceived;
  
    public UnitEventHandler OnActionSelectedInUnit;

    public TurnableEventHandler OnUpdateTurnTime;

    public LootCategory Loot;
    
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
        Inventory = GetComponent<UnitInventory>();
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

    public void OnHoverEnd()
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
         ReceiveDamage(new UnitEffect_Damage(5));
        
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
        int turncost = 0;
        var _currentAction = Actions.GetCurrentAction();
     
        if (_currentAction != null)
        {
            turncost += (int) _currentAction.GetTimeCost();
           
        }
        int cost = turncost + Actions.CurrentTurnCost;

        return cost;
    }

    public int GetTurnTime()
    {

        return (int) Stats.GetStatAmount(StatType.current_turn_time);
    }
   
    public void SetNextTurnTime(int turns)
    {
        if (_isDead) return;

        Stats.SetStatAmount(StatType.current_turn_time, Stats.GetStatAmount(StatType.current_turn_time) + turns);
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

        UnSelectCurrent();

        SelectedUnit = this;
        if (OnTurnStart != null) OnTurnStart(this);

    }

    public void GlobalTurn(int turn)
    {
        ActivationCheck();

        if (IsActive)
        {

            Stats.SetStatAmount(StatType.current_turn_time, Stats.GetStatAmount(StatType.current_turn_time) - 1);

            if (currentTile.isCamp)
                BaseCampTurn();            
        }
    }

    void BaseCampTurn()
    {

        Actions.RestCharges();
        Stats.Rest();       
       
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
     //   return gameObject.name+" ["+(GetTurnTime()+GetCurrentTurnCost()).ToString()+"]";
    }

    public Color GetColor()
    {
        return OwnerID == 0 ? Color.blue : Color.red;
    }
    public int GetTurnControllerID()
    {
        return OwnerID;
    }

    public void ReceiveDamage(UnitEffect_Damage dmg)
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
        return Mathf.Abs(firstPlayerUnit - enemyPos) <= Constants.UNIT_ACTIVATION_RANGE;
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
