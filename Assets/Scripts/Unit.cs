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
    public static UnitEventHandler OnUnitKilled;
    public static UnitEventHandler OnUnitHover;
    public static UnitEventHandler OnUnitSelect;

    public DamageEventHandler OnDamageReceived;
  
    public UnitEventHandler OnActionSelectedInUnit;

    public TurnableEventHandler OnUpdateTurnTime; 

    public UnitEventHandler OnTurnStart;
    public UnitEventHandler OnTurnEnded;

    public GameObject SelectedEffect;
    
    public bool PrePlaced = true;

  // [HideInInspector]
    public Tile currentTile;

    int AccCostCurrentturn;

    UI_Unit m_UI;
   
    
    public bool CanBeActivated()
    {
        return !isDead() && (OwnerID == 0 || ActiveRangeToPlayerUnits(this));
    }
    public bool IsActive
    {
        get { return _isActive; } set { _isActive = value; }
    }
    bool _isActive;

    public bool isDead()
    {
        return Stats.GetStat(UnitStats.Stats.will).current <= 0;
    }
    
    void Awake()
    {
        Stats = GetComponent<UnitStats>();

        Actions = GetComponent<ActionManager>();
        Actions.OnActionSelected += ActionChanged;
        Actions.OnActionUnselected += ActionChanged;

        AllUnits.Add(this);

        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
            b = gameObject.AddComponent<SelectibleObjectBase>();
       
        b.OnSelect += UnitSelected;
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;

        if (SelectedEffect != null) SelectedEffect.SetActive(false);

      
        m_UI = UI_Unit.CreateUnitUI();
        DisableUI();

        if(PrePlaced)
            SetTile(TileManager.Instance.GetClosestTile(transform.position), true);
    }

    void ActionChanged(UnitActionBase b)
    {
        if (OnUpdateTurnTime != null)
            OnUpdateTurnTime(this);
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
        _isActive = true;
        RegisterTurn();
    }
    void Start()
    {

        ActivationCheck();
            TurnSystem.Instance.OnGlobalTurn += GlobalTurn;
        
    }

    
    void DisableUI()
    {
        m_UI.gameObject.SetActive(false);
    }
    public void UpdateUI()
    {
        if (Stats.GetStat(UnitStats.Stats.will).current <= 0) return;
        m_UI.SetUnitInfo(this);
        m_UI.gameObject.SetActive(true);
    }
    public void OnHover()
    {
        if (OnUnitHover != null) OnUnitHover(this);
          UpdateUI();
    }

    void OnHoverEnd()
    {       
        if (TurnSystem.HasTurn(this)) return;
        DisableUI();
    }

    public void SetTile(Tile t, bool updatePosition )
    {
        if (currentTile != null)
        {
            currentTile.OnDeactivate -= OnTileCurrentDeactivate;
            if(currentTile.Child == this.gameObject)
            {
                currentTile.Child = null;
            }
        }
        if(t != null) { 
            currentTile = t;
            currentTile.OnDeactivate += OnTileCurrentDeactivate;
            t.SetChild(this.gameObject);
        }

        if (updatePosition)
        {
            transform.position = t.GetPosition();
        }
    }
   
    void OnTileCurrentDeactivate(Tile t)
    {
      //  Debug.Log("tile deactivate");
        if (t == currentTile) KillUnit();
    }
    void SetOwner(Player player)
    {
        SetOwner(player.PlayerID);
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
        //Fire Static event and let everyone know this unit has been selected/klicked
        //Debug.Log("unit selected");
        /** Cheat && Debug**/
        if(Input.GetKey(KeyCode.T))
         ReceiveDamage(new Damage());
        
        if (OnUnitSelect != null) OnUnitSelect(this);

        return;

    }        

    void UnselectUnit()
    {
        SelectedEffect.SetActive(false);
        DisableUI();
    }

    Player GetOwner()
    {
        return PlayerManager.GetPlayer(OwnerID)   ;
    }
    

    public void KillUnit()
    {
        if (OnUnitKilled != null) OnUnitKilled(this);
        Destroy(m_UI.gameObject);
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
            turncost += _currentAction.TurnTimeCost;
            //Debug.Log(_currentAction.ActionID+ " "+ _currentAction.TurnTimeCost);
        }
        return  turncost + Actions.CurrentTurnCost;
    }

    public int GetTurnTime()
    {
        //Debug.Log("ID "+GetID()+" "+time+ "  "+time);
        return TurnTime;
    }
   
    public void SetNextTurnTime(int turns)
    {     
        TurnTime = turns;
    }

    public void EndTurn()
    {

        if (OnTurnEnded != null) OnTurnEnded(this);
    }

    public void StartTurn()
    {
        starting_order++;

        Debug.Log("NEW TURN:" + GetID());  
        UI_ActiveUnit.Instance.SelectedUnitTF.text = GetID();
        UnSelectCurrent();
        PanCamera.FocusOnPlanePoint(currentTile.GetPosition());
       
        SelectedUnit = this;
        SelectedEffect.SetActive(true);
        UpdateUI();
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
    }

    void BaseCampTurn()
    {

        Actions.RestCharges();

        StatConfig intensity = Stats.GetStat(UnitStats.Stats.intensity);
        intensity.ModifyStat(-intensity.current);

        StatConfig will = Stats.GetStat(UnitStats.Stats.will);
        will.ModifyStat(will.max);


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

    public void ReceiveDamage(Damage dmg)
    {
        float mod = Constants.IncomingDamageModifier(Stats.GetStat(UnitStats.Stats.intensity).current);

        float dmg_received = - (dmg.amount + mod);
        float int_received = Mathf.Abs( dmg_received ) * Constants.RCV_DMG_TO_INT;
        Debug.Log(this.name + " rcv damge " + dmg_received + "  rcvd multiplier:"+ mod + "  +int="+int_received);


        Stats.GetStat(UnitStats.Stats.will).ModifyStat(dmg_received);
        ModifyInt(int_received);

        if (OnDamageReceived != null) OnDamageReceived(dmg);
        UpdateUI();
         
        if ( Stats.GetStat(UnitStats.Stats.will).current <= 0)
        {
            KillUnit();
        }
    }

    public void ModifyInt(float raw)
    {
        float received = raw;
     
        if(raw > 0) { 
            received = raw + IntModifier;        
        }
        Debug.Log("int received raw:" + raw.ToString() + " modified:" + received);
        Stats.GetStat(UnitStats.Stats.intensity).ModifyStat(received);
        UpdateUI();
    }
        
    float IntModifier
    {
        get
        {
            float current_will = Stats.GetStat(UnitStats.Stats.will).current;
            return current_will * Constants.WILL_TO_INT;
        }
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
