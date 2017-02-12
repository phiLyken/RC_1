using UnityEngine;
using System.Collections;
using UnityEditor;
using NUnit.Framework;


public class SquadManagerTest  {

    [Test]
    public void SquadManagerSingleTonTest()
    {  
        Assert.IsNotNull(SquadManager.Instance);
    }

    [Test]
    public void SquadManagerCreationTest()
    {
        SquadManager new_squadmanager = new SquadManager( new Levels(LevelTest.MockConfig())  );        
        Assert.True(new_squadmanager.GetMaxSquadsize() > 0 && new_squadmanager.GetUnlockedTiers().Count > 0, "SQUADSIZE INVALID || NO UNLOCKED UNITS");
    }

    [Test]
    public void SquadManagerHigherLevelTest()
    {
        Levels level = new Levels(LevelTest.MockConfig());
        SquadManager new_squadmanager = new SquadManager(level);

        int start_squad_size = new_squadmanager.GetMaxSquadsize();
        int start_selectibe_units = new_squadmanager.GetUnlockedTiers().Count;

        level.AddProgress(10000000);
        Assert.Less(start_selectibe_units, new_squadmanager.GetUnlockedTiers().Count, "DIDNT UNLOCK UNITS");
        Assert.Less(start_squad_size, new_squadmanager.GetMaxSquadsize(),"DIDNT UNLOCK SQUAD SIZE");
    }
}
