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

    PathDisplay pathpreview;
    void SetPreviewTile(Tile t)
    {
        List<Tile> pathToTile = TileManager.Instance.FindPath(Owner.currentTile, t);
        if(Owner.PathWalkable(pathToTile))
        {
            Debug.Log("asdasd");
            if(pathpreview != null)
            {
                Destroy(pathpreview.gameObject);
            }

            pathpreview = PathDisplay.MakePathDisplay();
            pathpreview.UpdatePositions(pathToTile);
            currentTargetTile = t;
            currentPath = pathToTile;
        }
    }
    protected override void ActionExecuted()
    {
        Owner.SetMovementTile(currentTargetTile, currentPath);


    }
    public override void UnSelectAction()
    {
        Debug.Log("asdasd");
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
