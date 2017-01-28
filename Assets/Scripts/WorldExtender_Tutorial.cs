using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldExtender_Tutorial  : WorldExtender{


    protected override void SetupGame()
    {
        CurrentStage = 0;
       
        spawned = new List<RegionConfig>();

        SpawnRegion(RegionLoader.GetStartRegion(RegionBalance), TileManager.Instance);
        SpawnNext();
    }

    public override void SpawnNext()
    {
        if(CurrentStage < RegionBalance.AllPools.Count)
        {
            RegionConfig region = RegionBalance.AllPools[0].Regions[CurrentStage].Region;
            SpawnRegion(region, TileManager.Instance);
            CurrentStage++;
        }
    }

}
