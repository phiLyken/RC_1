using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Move : UnitActionBase {


    Tile currentTargetTile;
    List<Tile> currentPath;

    public override void SelectAction()
    {
        TileCollectionHighlight.SetHighlight(GetWalkableTiles(Owner.currentTile), "selected");
        TileSelecter.OnTileSelect += SetMovementTile;
        TileSelecter.OnTileHover += SetPreviewTile;    
     
    }

    void SetMovementTile(Tile t)
    {

        if (t == currentTargetTile) 
            AttemptExection();        
    }
    void SetPreviewTile(Tile t)
    {
        List<Tile> pathToTile = TileManager.Instance.FindPath(Owner.currentTile, t);
        if(Owner.PathWalkable(pathToTile))
        {
            currentTargetTile = t;
            currentPath = pathToTile;

            foreach(Tile pt in pathToTile)
            {
                Debug.DrawRay(pt.transform.position, Vector3.up, Color.yellow, 2f);
            }
        }
    }
    protected override void ActionExecuted()
    {
        Owner.SetMovementTile(currentTargetTile, currentPath);


    }
    public override void UnSelectAction()
    {
        currentPath = null;
        currentTargetTile = null;

        TileSelecter.OnTileSelect -= SetMovementTile;
        TileSelecter.OnTileHover -= SetPreviewTile;

        TileCollectionHighlight.DisableHighlight();
    }
    
    bool CanWalkToTile(Tile t)
    {
        return true;
    }

    protected override bool CanExecAction()
    {
        return base.CanExecAction() && CanWalkToTile(currentTargetTile);
    }

    List<Tile> GetWalkableTiles(Tile t)
    {
        return TileManager.Instance.
            GetReachableTiles(t, 
            TileManager.Instance.GetTilesInRange(t, (int) Owner.Stats.GetStat(UnitStats.Stats.movement_range).current),
            Owner);
    }


}
