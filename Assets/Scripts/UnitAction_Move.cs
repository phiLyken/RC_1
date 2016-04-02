using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Move : UnitActionBase {
    
    Tile currentTargetTile;
    List<Tile> currentPath;

    public override void SelectAction()
    {
        base.SelectAction();
        TileCollectionHighlight.SetHighlight(GetWalkableTiles(Owner.currentTile), "selected");
        TileSelecter.OnTileSelect += SetMovementTile;
        TileSelecter.OnTileHover += SetPreviewTile;    
    
    }

    void SetMovementTile(Tile t)
    {
   
            AttemptExection();        
    }

    PathDisplay pathpreview;
    void SetPreviewTile(Tile t)
    {
        List<Tile> pathToTile = TileManager.Instance.FindPath(Owner.currentTile, t);
        if(Owner.PathWalkable(pathToTile))
        {

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
        base.ActionExecuted();
    }

    public override void UnSelectAction()
    {
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


    public List<Tile> GetWalkableTiles(Tile t)
    {
        return TileManager.Instance.
            GetReachableTiles(t, 
            TileManager.Instance.GetTilesInRange(t, (int) Owner.Stats.GetStat(UnitStats.Stats.movement_range).current),
            Owner);
    }


}
