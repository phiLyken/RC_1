using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void UnitEventHandler(Unit u);
public class Unit : MonoBehaviour, ITurn, IDamageable {

    int AP_Used = 99;
    int MaxAP = 2;
    public int TurnTime;

    public UnitActionBase[] Actions;
    UnitActionBase CurrentAction;

  //  [HideInInspector]
    public UnitStats Stats;
    
    public static List<Unit> AllUnits = new List<Unit>();
    public static Unit SelectedUnit;
    public static UnitEventHandler OnUnitKilled;
    public static UnitEventHandler OnUnitHover;
    public static UnitEventHandler OnUnitSelect;

    public UnitEventHandler OnTurnStart;
    public GameObject SelectedEffect;
    
    public int OwnerID;

  //  [HideInInspector]
    public Tile currentTile;

    UI_Unit m_UI;

    WaypointMover waypointMover;
   
    public int GetAPLeft()
    {
        return (MaxAP - AP_Used);
    }
    public bool HasAP(int ap)
    {
        return  GetAPLeft() >= ap;
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
       
        b.OnSelect += UnitSelected;
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
    public UnitActionBase SelectAbility(int index)
    {
        Debug.Log("Select Ability " + index);
      //  if (index > Actions.Length) return;
        if (SelectedUnit != this) return null;
        if (CurrentAction != null && CurrentAction == Actions[index])
        {
            UnsetCurrentAction();
            return null;
        }

        UnsetCurrentAction();
        if (Actions.Length <= index)
        {
            Debug.LogWarning("No ability #" + index);
            return null;
        }

        CurrentAction = Actions[index];
        CurrentAction.OnExecuteAction += OnActionUsed;
        CurrentAction.SelectAction();
        return CurrentAction;

    }

    private void UnsetCurrentAction()
    {
        if (CurrentAction == null) return;
        UI_ActiveUnit.Instance.AbilityTF.text = GetActionInfos();
        CurrentAction.UnSelectAction();
        CurrentAction.OnExecuteAction -= OnActionUsed;
        CurrentAction = null;
    }

    void OnActionUsed(UnitActionBase action)
    {        
        AP_Used += action.AP_Cost;
        TurnTime += action.TurnTimeCost;
       // Debug.Log(TurnTime);
        if(TurnSystem.HasTurn(this))
            PanCamera.Instance.PanToPos(currentTile.GetPosition());
        UnsetCurrentAction();
        Debug.Log( GetID()+" Action used" + action.ActionID);
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
        if(t != null) { 
            currentTile = t;
            currentTile.OnDeactivate += OnTileCurrentDeactivate;
        }
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
        Debug.Log("unit selected");
        /** Cheat && Debug**/
        if(Input.GetKey(KeyCode.T))
         ReceiveDamage(new Damage());
        
        if (OnUnitSelect != null) OnUnitSelect(this);

        return;
        /** Can probably be removed :
        if (!TurnSystem.HasTurn(this)) return;

        //If this unit h
        UnSelectCurrent();

        UpdateUI();
        SelectedUnit = this;
        SelectedEffect.SetActive(true);
       **/
    }        

    public void SetMovementTile(Tile target, List<Tile> path)
    {
        Debug.Log("set movement tile");
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
        return TurnTime;
    }

    public int GetNextTurnTime()
    {
     //   Debug.Log(TurnTime);
        return TurnTime;
    }
   
    public void SetNextTurnTime(int turns)
    {
        Debug.Log("Turn - setting next turn time " + turns);
        TurnTime = turns;
    }

    public void StartTurn()
    {
        Debug.Log("NEW TURN:" + GetID());  
        UI_ActiveUnit.Instance.SelectedUnitTF.text = GetID();
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

    public void ReceiveDamage(Damage dmg)
    {
       Stats.GetStat(UnitStats.Stats.will).ModifyStat(-dmg.amount);
        Debug.Log(this.name + " rcv damge " + dmg.amount);
       if( Stats.GetStat(UnitStats.Stats.will).current <= 0)
        {
            KillUnit();
        }
    }

    string GetActionInfos()
    {
        string s = "";
        for (int i = 0; i < Actions.Length; i++)
        {
            s += (i+1).ToString() + ":" + Actions[i].ActionID + "\n" ;
        }
        return s;
    }

    #region
    /// <summary>
    /// Returns a list of all the units of the requested owner, -1 will select all
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public static List<Unit> GetAllUnitsOfOwner(int owner)
    {
        List<Unit> list = new List<Unit>();
        foreach(Unit u in AllUnits)
        {
            if (owner == -1 || u.OwnerID == owner) list.Add(u);
        }

        return list;
    }


    #endregion
}
