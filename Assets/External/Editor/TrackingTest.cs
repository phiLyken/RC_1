using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using NUnit.Framework;
using UnityEngine;

public class TrackingTest  {

    [Test]
    public void MissionEndTest()
    {
        TrackingManager.TrackingCall_LevelComplete(
            Resources.Load<ScriptableRegionDataBaseConfigs>("Regions/RegionConfigs/selectablemissions_defaultbalancing").RegionConfigs.GetRandom(),
           new MissionOutcome(4, 2, 100, 1, 4, 0.5f, 3, 5),5);
    }

}
