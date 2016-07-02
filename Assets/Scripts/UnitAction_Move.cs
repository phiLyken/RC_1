﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Move : UnitActionBase {



    Tile currentTargetTile;
    List<Tile> currentPath;
    PathDisplay pathpreview;

    MeshViewGroup attack_preview_highlight;

    void Awake()
    {
        orderID = 0;
    }


    public override void SelectAction()
    {       
        base.SelectAction();

        if (Owner.GetComponent<WaypointMover>().Moving) return;
           
        TileSelecter.OnTileSelect += SetMovementTile;
        TileSelecter.OnTileHover += SetPreviewTile;

       // Debug.Log(TileSelecter.HoveredTile);  
        if(TileSelecter.HoveredTile != null)
        {
            SetPreviewTile(TileSelecter.HoveredTile);
        }
    }

    void SetMovementTile(Tile t)
    {
        if (!Owner.GetComponent<WaypointMover>().Moving && currentTargetTile != null && currentPath != null)
        {
         //   Debug.Log("set movement tile");
            AttemptExection();
        } else
        {
            Debug.LogWarning("Something prevented the move ability to execute");
        }
    }

    void SetAttackPreview(Tile t)
    {      
        ResetAttackPreview();
        
        VisualStateConfig attack_state = TileStateConfigs.GetMaterialForstate("attack_range_move_preview");
        List<Tile> in_range = (Owner.Actions.GetAcionOfType<UnitAction_ApplyEffectFromWeapon>().GetTargetableTilesForUnit(t));
        List<Tile> border = TileManager.FindBorderTiles(in_range, TileManager.Instance,true);
        attack_preview_highlight = new MeshViewGroup(border, attack_state);
    }
    void ResetAttackPreview()
    {
        if(attack_preview_highlight != null) { 
             attack_preview_highlight.RemoveGroup();
        }
    }
    void SetPreviewTile(Tile t)
    {
       // Debug.Log("setpreview tile");
		//TODO: Send a filter list custom to the unit e.g. Enemies should walk on camp tiles, Tiles need properties as components
        List<Tile> pathToTile = TileManager.Instance.FindPath(Owner.currentTile, t, Owner);
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
            SetAttackPreview(currentTargetTile);

        } else
        {
            ResetAttackPreview();
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
       
        // ActionCompleted();

    }

    void OnMoveEnd(IWayPoint wp)
    {
        ActionCompleted();
        Owner.GetComponent<WaypointMover>().OnMovementEnd -= OnMoveEnd;
    }

    public bool PathWalkable(List<Tile> p)
    {
        float MoveRange = GetMoveRange();
        return p != null &&  p.Count > 1 && TilePathFinder.GetPathLengthForUnit(Owner, p) <= MoveRange;
    }

    public void SetMovementTile(Tile target, List<Tile> path)
    {
        Owner.SetTile(target, false);

        WaypointMover mover = Owner.GetComponent<WaypointMover>();
        mover.MoveOnPath(path, 3);

        mover.OnMovementEnd += OnMoveEnd;
    }

    public List<Tile> GetReachableTiles(Tile from, List<Tile> tiles, Unit unit)
    {
        List<Tile> reacheable = new List<Tile>();
        foreach (Tile t in tiles)
        {
			List<Tile> path = TileManager.Instance.FindPath(from, t,unit );
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
        ResetAttackPreview();
        
    }

    public override List<Tile> GetPreviewTiles()
    {
        return GetWalkableTiles(Owner.currentTile);
    }
    public List<Tile> GetWalkableTiles(Tile origin)
    {
        int moveRange = (int)GetMoveRange();
        return  GetReachableTiles(origin, 
            TileManager.Instance.GetTilesInRange(origin, moveRange),
            Owner);
    }

    public float GetMoveRange()
    {
        return (Owner.Stats as PlayerUnitStats).MoveRange;
    }

}
