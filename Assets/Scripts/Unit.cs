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

    public UnitEventHandler OnTurnStart;
    public GameObject SelectedEffect;
    
    public int OwnerID;

    [HideInInspector]
    public Tile currentTile;

    UI_Unit m_UI;

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
        b.OnHover += OnHover;
        b.OnHoverEnd += OnHoverEnd;

        if (SelectedEffect != null) SelectedEffect.SetActive(false);

        Actions = GetComponentsInChildren<UnitActionBase>();
        foreach (UnitActionBase action in Actions) action.SetOwner(this);
        m_UI = UI_Unit.CreateUnitUI();
        DisableUI();

        SetTile(TileManager.Instance.GetClosestTile(transform.position));
        transform.position = currentTile.GetPosition();

        RegisterTurn();
    }

    void DisableUI()
    {
        m_UI.gameObject.SetActive(false);
    }
    public void UpdateUI()
    {
        m_UI.SetUnitInfo(this);
        m_UI.gameObject.SetActive(true);
    }
    void OnHover()
    {
          UpdateUI();
    }

    void OnHoverEnd()
    {
       
        if (TurnSystem.HasTurn(this)) return;


        DisableUI();
    }

    void WaypointReached(IWayPoint p)
    {

    }

    void Update()
    {
        if (OwnerID != 0) return;
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
    public void SelectAbility(int index)
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

        UpdateUI();
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
        DisableUI();
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
        Destroy(m_UI.gameObject);
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
     //   Debug.Log("Turn - setting next turn time " + turns);
        TurnTime = turns;
    }

    public void StartTurn()
    {
      //  Debug.Log("Turn in unit start " + GetID());
        UnSelectCurrent();
        PanCamera.FocusOnPlanePoint(currentTile.GetPosition());
        AP_Used = 0;
        SelectedUnit = this;
        SelectedEffect.SetActive(true);
        UpdateUI();
        if (OnTurnStart != null) OnTurnStart(this);    
    }

    public void GlobalTurn()
    {
        TurnTime--;
    }

    public bool HasEndedTurn()
    {
        return AP_Used >= MaxAP && !waypointMover.Moving;
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

    public int GetTurnControllerID()
    {
        return OwnerID;
    }
}
