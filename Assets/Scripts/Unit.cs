using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void UnitEventHandler(Unit u);
public class Unit : MonoBehaviour {

    public static List<Unit> AllUnits = new List<Unit>();
    public static Unit SelectedUnit;
    public static UnitEventHandler OnUnitKilled;

    public GameObject SelectedEffect;

    public  int MovementDistance;
    bool HasMoved;
    
    public float Speed;
    public int OwnerID;

    Tile currentTile;

   
    WaypointMover waypointMover;

    void Awake()
    {
        AllUnits.Add(this);
        waypointMover = GetComponent<WaypointMover>();
      //  waypointMover.OnWayPointreached +=
        SelectibleObjectBase b = GetComponent<SelectibleObjectBase>();
        if (b == null)
            b = gameObject.AddComponent<SelectibleObjectBase>();
       
        b.OnSelect += SelectUnit;
        if (SelectedEffect != null) SelectedEffect.SetActive(false);

    }
    void WaypointReached(IWayPoint p)
    {

    }

    void Start()
    {
        TurnSystem.Instance.OnTurnStart += OnStartTurn;
        SetTile(TileManager.Instance.GetClosestTile(transform.position));
        transform.position = currentTile.GetPosition();
    }
    void SetTile(Tile t)
    {
        if (currentTile != null) currentTile.OnDeactivate -= OnTileCurrentDeactivate;
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
  
    void OnStartTurn(int turn)
    {
        HasMoved = false;
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
        bool selectedWasThis = SelectedUnit != null && SelectedUnit == this;

        UnSelectCurrent();

        if (selectedWasThis || HasEndedTurn()) return;

        SelectedUnit = this;
        SelectedEffect.SetActive(true);
        TileCollectionHighlight.SetHighlight(GetWalkableTiles(), "selected");
        TileSelecter.OnTileSelect += SetMovementTile;
    }        

    public bool PathWalkable(List<Tile> p)
    {
        return p != null && p.Count - 1 <= MovementDistance && p.Count > 1;
    }

    void SetMovementTile(Tile t)
    {
        List<Tile> path = TileManager.FindPath(TileManager.Instance, currentTile, t);
        if (PathWalkable(path))
        {
            SetTile(t);
            t.SetChild(this.gameObject);
            waypointMover.MoveOnPath(path, Speed);
            HasMoved = true;
            UnSelectCurrent();
        }
    }
     List<Tile> GetWalkableTiles()
    {
        return   TileManager.Instance.GetReachableTiles( currentTile, TileManager.Instance.GetTilesInRange(currentTile, MovementDistance), this);
    }
    void UnselectUnit()
    {
        SelectedEffect.SetActive(false);
        TileSelecter.OnTileSelect -= SetMovementTile;
        TileCollectionHighlight.DisableHighlight();
    }
    public bool HasEndedTurn()
    {
        return HasMoved && !waypointMover.Moving;
    }

    public void OnUnitSelected()
    {
        if (TurnSystem.Instance.PlayerHasTurn(GetOwner())) {
                TileManager.Instance.GetTilesInRange(currentTile, MovementDistance);
        }
    }

    Player GetOwner()
    {
        return TurnSystem.Instance.GetPlayer(OwnerID)   ;
    }
    public void OnUnitDeselected()
    {
        if(TurnSystem.Instance.PlayerHasTurn(GetOwner()))
        {

        }
    }

    public void KillUnit()
    {
        if (OnUnitKilled != null) OnUnitKilled(this);
        GetOwner().RemoveUnit(this);
        AllUnits.Remove(this);

        Destroy(this.gameObject);
        
    }
}
