using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_Loot : UnitActionBase
{
 
    public float Range;


    public override void SelectAction()
    {
        base.SelectAction();

        if (OnTargetsFound != null)
        {
            
            OnTargetsFound(GetLootableTiles().Select(t => t.gameObject).ToList());
        }

        TileSelecter.OnTileSelect += OnTileSelect;
        TileSelecter.SetUnitColliders(false);

        TileSelecter.OnTileHover += OnTileHover;
        TileSelecter.OnTileUnhover += OnTileUnhover;
    }



    void OnTileHover(Tile tgt)
    {

       if(GetLootableTiles().Contains(tgt) && OnTargetHover != null)
        {
            OnTargetHover(tgt);
        }
    }

    void OnTileUnhover(Tile tgt)
    {
        if (GetLootableTiles().Contains(tgt) && OnTargetUnhover != null)
        {
            OnTargetUnhover(tgt);
        }
    }
    public void OnTileSelect(Tile selected_tile)
    {
        if(!GetLootableTiles().Contains(selected_tile))
        {
            ToastNotification.SetToastMessage2("No Loot on this Tile");
            return;
        }       

        Tile_Loot loot = selected_tile.GetComponent<Tile_Loot>();

        if (loot.GetLootableAmount(Owner) == 0 )
        {
            ToastNotification.SetToastMessage2("Unit has no space for item " + loot.GetLootType() + " " + loot.GetLootableAmount(Owner));
            return;
        }

       
        AttemptAction(selected_tile);
    }

    public override List<Tile> GetPreviewTiles()
    {
        return   TileManager.Instance.GetTilesInRange(Owner.currentTile, (int)Range);
    } 

    public override void UnSelectAction()
    {
        base.UnSelectAction();
        TileSelecter.OnTileSelect-= OnTileSelect;
        TileSelecter.SetUnitColliders(true);
    }

    public List<Tile> GetLootableTiles()
    {
        return TileManager.Instance.GetTilesInRange(Owner.currentTile, (int)Range).Where(t => t.GetComponent<Tile_Loot>() != null).ToList();
    }

    public override bool CanExecAction(bool b)
    {
        if(GetLootableTiles().Count == 0)
        {
            if(b)ToastNotification.SetToastMessage2("No Loot Nearby");
            return false;
        }
        return base.CanExecAction(b);
    }


    protected override void ActionExecuted(Component target)
    {   
 
        Tile_Loot l = target.GetComponent<Tile_Loot>();
        l.OnLoot(Owner);

        StartCoroutine(DelayedCompletion(1f));

    }
 
      

}
