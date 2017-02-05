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



    protected override void SetupGame()
    {
        CurrentStage = 0;
       
        spawned = new List<RegionConfig>();

        SpawnRegion(RegionLoader.GetStartRegion(RegionBalance), TileManager.Instance);
        SpawnNext();


    }

    public void OnComplete(Objective obj)
    {
        if(obj.GetSaveID() == "loot")
        {
            SpawnNext();
            SpawnNext();
        }
    }
    public override void SpawnNext()
    {
        if(CurrentStage < RegionBalance.AllPools[0].Regions.Count)
        {
            RegionConfig region = RegionBalance.AllPools[0].Regions[CurrentStage].Region;
            SpawnRegion(region, TileManager.Instance);
            
            CurrentStage++;
        }
    }

}
