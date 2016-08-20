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
       
        if(OnTargetsFound != null)
        {
            OnTargetsFound(GetLootableTiles().Select(t => t.gameObject).ToList());
        }
    }


    Tile FIXME_selected;

    public void OnTileSelect(Tile t)
    {
        if(GetLootableTiles().Contains(t))
        {
            FIXME_selected = t;
            AttemptExection();
        } else
        {
            ToastNotification.SetToastMessage2("No Loot on this Tile");
        }
    }

    public override List<Tile> GetPreviewTiles()
    {
        return   TileManager.Instance.GetTilesInRange(Owner.currentTile, (int)Range);
    }

    public override void UnSelectAction()
    {
        base.UnSelectAction();
        TileSelecter.OnTileSelect-= OnTileSelect;
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
        base.ActionExecuted();
        if (OnTarget != null)
            OnTarget(this, FIXME_selected.transform);

        Tile_Loot l = FIXME_selected.GetComponent<Tile_Loot>();
        l.OnLoot(Owner);

    
        ActionCompleted();
    }

}
