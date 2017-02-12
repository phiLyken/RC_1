using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public delegate void UnitEventHandler(Unit u);
public class Unit : MonoBehaviour, ITurn, IDamageable {

    int starting_order;
    public int OwnerID;
    public ScriptableUnitConfig Config;
    public UnitStats Stats;
    public ActionManager Actions;
 
    [HideInInspector]
    public UnitInventory Inventory;

    public static List<Unit> AllUnits = new List<Unit>();
    public static Unit SelectedUnit;

    public UnitEventHandler OnIdentify;
    
    public static Unit HoveredUnit;
    public static UnitEventHandler OnUnitKilled;
    public static UnitEventHandler OnUnitHover;
    public static UnitEventHandler OnUnitHoverEnd;
    public static UnitEventHandler OnUnitSelect;
    public static UnitEventHandler OnTurnStart;
    public static UnitEventHandler OnTurnEnded;
    public static UnitEventHandler OnEvacuated;

    public DamageEventHandler OnDamageReceived;
  
    public UnitEventHandler OnActionSelectedInUnit;

    public Action<ITurn> OnUpdateTurnTime;

    public EnemyDropCategory Loot;

    Sprite UnidentifiedSprite;
    Sprite FaceSprite;

    [HideInInspector]
    public Tile currentTile;
        
    public bool CanBeActivated()
    {
        return  (OwnerID == 0 || ActiveRangeToPlayerUnits(this));
    }
    public bool IsIdentified
    {
        get { return _isIdentified; }
       
    }
    public bool IsActive
    {
        get {  return _isActive; } set { _isActive = value; }
    }
    bool _isActive;
    bool _isDead;
    bool _isIdentified;
    bool _isEvacuated;

    public event System.Action OnUpdateSprite;

    public void SetSprite(Sprite face, Sprite unidentified)
    {
        FaceSprite = face;
        UnidentifiedSprite = unidentified;
    }
    void Awake()
    {
        Inventory = GetComponent<UnitInventory>();
        Stats = GetComponent<UnitStats>();
        Stats.OnHPDepleted += KillUnit;

        Actions = GetComponent<ActionManager>();
        Actions.SetOwner(this);
        Actions.OnActionSelected += ActionChanged;
        Actions.OnActionUnselected += ActionChanged;
 
        AllUnits.Add(this);

        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
            b = gameObject.AddComponent<SelectibleObjectBase>();
       
        b.OnSelect += UnitSelected;
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;              
        
      
    }

  
    void ActionChanged(UnitActionBase b)
    {
        if (OnUpdateTurnTime != null)
            OnUpdateTurnTime( this as ITurn);
    }

    public bool IsDead()
    {
        return _isDead;
    }
    public bool IsEvacuated()
    {
        return _isEvacuated;
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

    public void Identify(Unit identifier)
    {

        if (IsIdentified)
            return;

        Debug.Log("^unit IDENTIFIED");
        _isIdentified = true;
        gameObject.transform.FindDeepChild("playermodel").gameObject.GetComponentsInChildren<Renderer>().ToList().ForEach(rend => rend.enabled = true);

        if (OnIdentify != null)
            OnIdentify(identifier);

        OnUpdateSprite.AttemptCall();
        GetComponent<Collider>().enabled = true;
    }

    public void SetColliderState(bool b)
    {
        Collider c = GetComponent<Collider>();
        c.enabled = !_isIdentified || b;
    }
    void Start()
    {
        ActivationCheck();
        if(TurnSystem.Instance != null)
        TurnSystem.Instance.OnGlobalTurn += GlobalTurn;        
    }    

    public void OnHover()
    {
        if (!_isIdentified)
            return;

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

    void UnSelectCurrent()
    {
        if(SelectedUnit != null)
        {
            SelectedUnit.UnselectUnit();
            SelectedUnit = null;
        }
    }

    public bool Evacuate()
    {
        if (!currentTile.isCamp)
            return false;
        _isEvacuated = true;
        Debug.Log("^unit EVAC " + this.ToString());

        RemoveUnitFromGame();

        if (OnEvacuated != null)
            OnEvacuated(this);
        
        return true;
    }
    public void UnitSelected()
    {
        if(Input.GetKey(KeyCode.T))
             ReceiveDamage(new UnitEffect_Damage(10));

        if (Input.GetKey(KeyCode.Z))
             ReceiveDamage(new UnitEffect_Damage(1));

        if (Input.GetKey(KeyCode.U))
        {
            List<Unit> enemies = Unit.GetAllUnitsOfOwner(1,true);
            enemies.GetRandom().GetComponent<UnitAI>().SetPreferredTarget(this);
        }

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
        if (!_isDead)
        {
            Debug.Log("^unit " + GetID());
            Stats.OnHPDepleted -= KillUnit;
            _isDead = true;

            if (OnUnitKilled != null)
                OnUnitKilled(this);

            RemoveUnitFromGame();
        }      

    }
 
    void OnDestroy()
    {
        currentTile.OnDeactivate -= OnCurrentTileDeactivate;

        AllUnits.Remove(this);
    }
    public void RemoveUnitFromGame()
    {
        TurnSystem.Instance.OnGlobalTurn -= GlobalTurn;
        TurnSystem.Unregister(this);

        if (GetOwner() !=null)
            GetOwner().RemoveUnit(this);
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

    public float GetTurnTime()
    {
       // Debug.Log(Stats.GetStatAmount(StatType.current_turn_time));
        return  Stats.GetStatAmount(StatType.current_turn_time);
    }
   
    public void SetNextTurnTime(float turns)
    {
        if (_isDead) return;

        Stats.SetStatAmount(StatType.current_turn_time, Stats.GetStatAmount(StatType.current_turn_time) + turns);
    }

    public void EndTurn()
    {
        SetNextTurnTime(GetCurrentTurnCost());
        Actions.Reset();
        Debug.Log("^turnSystem ENDED:" + GetID());

        if (OnTurnEnded != null) OnTurnEnded(this);
    }

    public void StartTurn()
    {
        starting_order++;

        Debug.Log("^turnSystem NEW TURN:" + GetID());         

        UnSelectCurrent();

        
        if (OnTurnStart != null) OnTurnStart(this);


        if (IsActive)
        {
            SelectedUnit = this;
        }
        else if(OwnerID == 0)
        {
            SkipTurn();
        }

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

        return true;
    }    

    public void UnRegisterTurn()
    {
        TurnSystem.Unregister(this);
    }

    public string GetID()
    {
        return gameObject.name;   
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
    public static List<Unit> GetAllUnitsOfOwner(int owner, bool include_units_in_basecamp)
    {
        List<Unit> list = new List<Unit>();
        foreach(Unit u in AllUnits)
        {

                                                                    
            if ( !u.IsEvacuated() && !u.IsDead() && (owner == -1 || u.OwnerID == owner) && ( include_units_in_basecamp  || !u.currentTile.isCamp) )
            {
                list.Add(u);
            }
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

 
    public static Vector3 GetHeadPos(Unit u)
    {
        //   return u.transform.position;
        Transform head = u.transform.FindChildWithTag("Head");

        if(head != null)
        {
            return head.GetComponent<Renderer>().bounds.center;
        }

        return u.transform.position + Vector3.up * 1.5f;
    }

    public void SkipTurn()
    {
        Actions.SkipTurn();
    }     

    public Action<ITurn> TurnTimeUpdated
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

    public Sprite GetIcon()
    {
        return _isIdentified ? FaceSprite : UnidentifiedSprite;
    }
}
