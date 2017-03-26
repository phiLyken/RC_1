using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldExtender_Tutorial  : WorldExtender{

    public void SetTutorialState()
    {
        MissionSystem.OnCompleteMission += OnComplete;

 
        if ( MissionSystem.HasCompletedGlobal("find_enemy"))
        {
            Unit.GetAllUnitsOfOwner(0, true)[0].Identify(null);
        }

        if (MissionSystem.HasCompletedGlobal("move_to"))
        {
            Unit.GetAllUnitsOfOwner(1, true)[0].Identify(Unit.GetAllUnitsOfOwner(0, true)[0]);
            Tile close_to_enemy = TileManager.Instance.GetEdgeTiles(M_Math.GetListFromObject(Unit.GetAllUnitsOfOwner(1, true)[0].currentTile), 1, TileManager.Instance).First();
            Unit.GetAllUnitsOfOwner(0, true)[0].SetTile(close_to_enemy, true);
        }

        if (MissionSystem.HasCompletedGlobal("loot"))
        {            
            SpawnNext();
            SpawnNext();

            List<Tile> lastRow = TileManager.Instance.GetRow(TileManager.Instance.GridHeight-1);

            Tile r = lastRow.FirstOrDefault(t => t.isAccessible);

            Unit.GetAllUnitsOfOwner(0, true)[0].SetTile(r, true);
           ;
        }
    }



    public override void SetupGame(RegionConfigDataBase region)
    {
        Instance = this;
     
        RegionBalance = region;
        spawned = new List<RegionConfig>();
        SetSpawnConfigs(region);
        SpawnNext();
        SpawnNext();
        PlayAmbient();

    }

    public void OnComplete(Objective obj)
    {
        if(obj.GetSaveID() == "loot")
        {
            SpawnNext();
            SpawnNext();
        }
    }


}
