using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Move : UnitActionBase {

    public int MoveRange;

    Tile currentTargetTile;
    List<Tile> currentPath;
    PathDisplay pathpreview;

    void Awake()
    {
        orderID = 0;
    }

    public override void SelectAction()
    {       
        base.SelectAction();

        if (Owner.GetComponent<WaypointMover>().Moving) return;
      
        TileCollectionHighlight.SetHighlight(GetWalkableTiles(Owner.currentTile), "selected");
        TileSelecter.OnTileSelect += SetMovementTile;
        TileSelecter.OnTileHover += SetPreviewTile;

        Debug.Log(TileSelecter.HoveredTile);  
        if(TileSelecter.HoveredTile != null)
        {
            SetPreviewTile(TileSelecter.HoveredTile);
        }
    }

    void SetMovementTile(Tile t)
    {
        if (!Owner.GetComponent<WaypointMover>().Moving && currentTargetTile != null && currentPath != null)
        {
            Debug.Log("set movement tile");
            AttemptExection();
        } else
        {
            Debug.LogWarning("Something prevented the move ability to execute");
        }
    }

    
    void SetPreviewTile(Tile t)
    {
       // Debug.Log("setpreview tile");
        List<Tile> pathToTile = TileManager.Instance.FindPath(Owner.currentTile, t);
        if(PathWalkable(pathToTile))
        {
            if(pathpreview != null)
            {
                Destroy(pathpreview.gameObject);
            }

            pathpreview = PathDisplay.MakePathDisplay();
            pathpreview.UpdatePositions(pathToTile);
            currentTargetTile = t;
            currentPath = pathToTile;
        } else
        {
            currentPath = null;
            currentTargetTile = null;
          //  Debug.Log("cannot move to tile");
        }
    }
    protected override void ActionExecuted()
    {

        // Debug.Log("move executed");
        ActionInProgress = true;
        SetMovementTile(currentTargetTile, currentPath);
       
      
        base.ActionExecuted();
        UnSelectAction();
        // ActionCompleted();

    }
    public bool PathWalkable(List<Tile> p)
    {
        return p != null && p.Count - 1 <= MoveRange && p.Count > 1;
    }

    public void SetMovementTile(Tile target, List<Tile> path)
    {
        Debug.Log("set movement tile");
        Owner.SetTile(target, false);

        WaypointMover mover = Owner.GetComponent<WaypointMover>();
        mover.MoveOnPath(path, 3);

        mover.OnMovementEnd += Waypoint => { ActionCompleted(); };

    }

    public List<Tile> GetReachableTiles(Tile from, List<Tile> tiles, Unit unit)
    {
        List<Tile> reacheable = new List<Tile>();
        foreach (Tile t in tiles)
        {
            List<Tile> path = TileManager.Instance.FindPath(from, t);
            if (PathWalkable(path)) reacheable.Add(t);
        }

        return reacheable;
    }


    public override void UnSelectAction()
    {
        base.UnSelectAction();
        currentPath = null;
        currentTargetTile = null;

        TileSelecter.OnTileSelect -= SetMovementTile;
        TileSelecter.OnTileHover -= SetPreviewTile;
        if (pathpreview != null)
        {
            Destroy(pathpreview.gameObject);
        }

        TileCollectionHighlight.DisableHighlight();
    }


    public List<Tile> GetWalkableTiles(Tile origin)
    {
        return  GetReachableTiles(origin, 
            TileManager.Instance.GetTilesInRange(origin,MoveRange),
            Owner);
    }


}
