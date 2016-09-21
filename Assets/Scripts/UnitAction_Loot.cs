using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_Loot : UnitActionBase
{
    public float Range;
    void Awake()
    {
        orderID = 99;
    }

    public override void SelectAction()
    {
        base.SelectAction();

        TileSelecter.OnTileSelect += OnTileSelect;
        TileSelecter.EnablePositionMarker(true);

        if(OnTargetsFound != null)
        {
            OnTargetsFound(GetLootableTiles().Select(t => t.gameObject).ToList());
        }
    }


    Tile FIXME_selected;

    public void OnTileSelect(Tile t)
    {
        if(!GetLootableTiles().Contains(t))
        {
            ToastNotification.SetToastMessage2("No Loot on this Tile");
            return;
        }

        FIXME_selected = t;

        Tile_Loot loot = FIXME_selected.GetComponent<Tile_Loot>();

        if (loot.GetLootableAmount(Owner) == 0 )
        {
            ToastNotification.SetToastMessage2("Unit has no space for item " + loot.GetLootType() + " " + loot.GetLootableAmount(Owner));
            return;
        }

       
        AttemptExection();
    }

    public override List<Tile> GetPreviewTiles()
    {
        return   TileManager.Instance.GetTilesInRange(Owner.currentTile, (int)Range);
    } 

    public override void UnSelectAction()
    {
        base.UnSelectAction();
        TileSelecter.OnTileSelect-= OnTileSelect;
        TileSelecter.EnablePositionMarker(false);
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

    protected override void ActionExecuted()
    {
        ActionInProgress = true;
        if (OnTarget != null)
        {
 
            OnTarget(this, FIXME_selected.transform);
        }
        base.ActionExecuted();

        StartCoroutine(DelayedEnd());
 


       
    }

    IEnumerator DelayedEnd()
    {

        yield return new WaitForSeconds(0.35f);
        Tile_Loot l = FIXME_selected.GetComponent<Tile_Loot>();
        l.OnLoot(Owner);
        yield return new WaitForSeconds(0.5f);

        ActionCompleted();
    }
      

}
