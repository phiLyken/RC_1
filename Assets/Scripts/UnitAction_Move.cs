using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAction_Move : UnitActionBase {

    public int IntGrowthPerMove;

    Tile currentTargetTile;
    List<Tile> currentPath;

    public override void SelectAction()
    {
        

        base.SelectAction();

        if (Owner.GetComponent<WaypointMover>().Moving) return;
        TileCollectionHighlight.SetHighlight(GetWalkableTiles(Owner.currentTile), "selected");
        TileSelecter.OnTileSelect += SetMovementTile;
        TileSelecter.OnTileHover += SetPreviewTile;    
    
    }

    void SetMovementTile(Tile t)
    {
        //Debug.Log("set movement tile");
        AttemptExection();        
    }

    PathDisplay pathpreview;
    void SetPreviewTile(Tile t)
    {
       // Debug.Log("setpreview tile");
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
        Owner.SetMovementTile(currentTargetTile, currentPath);
        Owner.ModifyInt(IntGrowthPerMove);

        base.ActionExecuted();

    }

    protected override bool CanExecAction()
    {    
        return base.CanExecAction() && !Owner.GetComponent<WaypointMover>().Moving && currentTargetTile != null && currentPath != null;
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
        return TileManager.Instance.
            GetReachableTiles(origin, 
            TileManager.Instance.GetTilesInRange(origin, (int) Owner.Stats.GetStat(UnitStats.Stats.movement_range).current),
            Owner);
    }


}
