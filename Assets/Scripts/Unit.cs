using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void UnitEventHandler(Unit u);
public class Unit : MonoBehaviour, ITurn {

    int AP_Used = 99;
    int MaxAP = 1;
    int TurnTime;

    public UnitActionBase[] Actions;
    UnitActionBase CurrentAction;

    [HideInInspector]
    public UnitStats Stats;
    
    public static List<Unit> AllUnits = new List<Unit>();
    public static Unit SelectedUnit;
    public static UnitEventHandler OnUnitKilled;

    public GameObject SelectedEffect;
    
    public int OwnerID;

    [HideInInspector]
    public Tile currentTile;
    
    WaypointMover waypointMover;
    public bool HasAP(int ap)
    {
        return (MaxAP - AP_Used) >= ap;
    }
    void Awake()
    {
        Stats = GetComponent<UnitStats>();
       
        AllUnits.Add(this);
        waypointMover = GetComponent<WaypointMover>();
        TurnSystem.Register(this);
        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
            b = gameObject.AddComponent<SelectibleObjectBase>();
       
        b.OnSelect += SelectUnit;
        if (SelectedEffect != null) SelectedEffect.SetActive(false);

        Actions = GetComponentsInChildren<UnitActionBase>();
        foreach (UnitActionBase action in Actions) action.SetOwner(this);

        RegisterTurn();
    }
    void WaypointReached(IWayPoint p)
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SelectAbility(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectAbility(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SelectAbility(2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SelectAbility(3);
        }
    }
    void SelectAbility(int index)
    {
        if (SelectedUnit != this) return;
        if (CurrentAction != null && CurrentAction == Actions[index])
        {
            UnsetCurrentAction();
            return;
        }

        UnsetCurrentAction();
        if (Actions.Length <= index)
        {
            Debug.LogWarning("No ability #" + index);
            return;
        }

        CurrentAction = Actions[index];
        CurrentAction.OnExecuteAction += OnActionUsed;
        CurrentAction.SelectAction();

    }

    private void UnsetCurrentAction()
    {
        if (CurrentAction == null) return;

        CurrentAction.UnSelectAction();
        CurrentAction.OnExecuteAction -= OnActionUsed;
        CurrentAction = null;
    }

    void OnActionUsed(UnitActionBase action)
    {

        AP_Used += action.AP_Cost;
        UnsetCurrentAction();
    }
    void Start()
    {       
        SetTile(TileManager.Instance.GetClosestTile(transform.position));
        transform.position = currentTile.GetPosition();
    }
    void SetTile(Tile t)
    {
        if (currentTile != null)
        {
            currentTile.OnDeactivate -= OnTileCurrentDeactivate;
            if(currentTile.Child == gameObject)
            {
                currentTile.Child = null;
            }
        }
        currentTile = t;
        currentTile.OnDeactivate += OnTileCurrentDeactivate;
    }
   
    void OnTileCurrentDeactivate(Tile t)
    {
        Debug.Log("tile deactivate");
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
  


    void UnSelectCurrent()
    {
        if(SelectedUnit != null)
        {
            SelectedUnit.UnselectUnit();
            SelectedUnit = null;
        }
    }
    void SelectUnit()
    {
        if (!TurnSystem.HasTurn(this)) return;
        UnSelectCurrent();
       
        SelectedUnit = this;
        SelectedEffect.SetActive(true);
       
    }        

    public void SetMovementTile(Tile target, List<Tile> path)
    {       
        SetTile(target);
        target.SetChild(this.gameObject);
        waypointMover.MoveOnPath(path, 3);
    }

    void UnselectUnit()
    {
        UnsetCurrentAction();
        SelectedEffect.SetActive(false);
 
    }

    Player GetOwner()
    {
        return PlayerManager.GetPlayer(OwnerID)   ;
    }


    public bool PathWalkable(List<Tile> p)
    {
        return p != null && p.Count - 1 <= Stats.GetStat(UnitStats.Stats.movement_range).current && p.Count > 1;
    }

    public void KillUnit()
    {
        if (OnUnitKilled != null) OnUnitKilled(this);
        GetOwner().RemoveUnit(this);
        TurnSystem.Unregister(this);
        AllUnits.Remove(this);

        Destroy(this.gameObject);        
    }

    public int GetTurnTimeCost()
    {
        return 2;
    }

    public int GetNextTurnTime()
    {
        return TurnTime ;
    }
   
    public void SetNextTurnTime(int turns)
    {
        Debug.Log("Turn - setting next turn time " + turns);
        TurnTime = turns;
    }

    public void StartTurn()
    {
        Debug.Log("Turn in unit start " + GetID());
        UnSelectCurrent();
        AP_Used = 0;
        SelectedUnit = this;
        SelectedEffect.SetActive(true);      
    }

    public void GlobalTurn()
    {
        TurnTime--;
    }

    public bool HasEndedTurn()
    {
        return AP_Used >= MaxAP;
    }

    public void RegisterTurn()
    {
        TurnSystem.Register(this);
    }

    public void UnRegisterTurn()
    {
        TurnSystem.Unregister(this);
    }

    public string GetID()
    {
        return gameObject.name+" ["+TurnTime+"]";
    }
}
