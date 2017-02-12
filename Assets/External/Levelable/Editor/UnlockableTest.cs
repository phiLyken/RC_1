using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using NUnit.Framework;

public class UnlockableTest
{
    /// <summary>
    /// Creates Levels, and adds Unlockables after leveling has happend, should still unlock
    /// </summary>
    [Test]
    public void TestDelayedLevelUpListener()
    {
        Levels levels = MockLevels();
      
        levels.AddProgress(0);
        levels.AddProgress(30);
        levels.AddProgress(31);
        levels.AddProgress(100);
        levels.AddProgress(100000000);

        List<Unlockable<BlaConfig>> Unlockables = UnlockableFactory.MakeUnlockables(MockConfigs(), levels, getunlock, getid );

        Unlockables.ForEach(unlock => unlock.AddUnlockListener(OnUnlock));

        bool _all_called = HasAllUnlockedOnce(Unlockables);

        Assert.IsTrue(_all_called, "Not All Configs have been unlocked");
    }

    /// <summary>
    /// Creating levels, creating unlocks and level up while they are listing
    /// </summary>
    [Test]
    public void TestLevelUpCallBacks()
    {
        Levels levels = MockLevels();
        List<Unlockable<BlaConfig>>  Unlockables = UnlockableFactory.MakeUnlockables(MockConfigs(), levels, getunlock, getid, OnUnlock);
        levels.OnProgress += p => Debug.Log("PROGRESS " + p + " " + levels.GetProgressInLevel());
        levels.OnLevelUp += l => Debug.Log("LEVELUP " + l);

        //GetComponent<UnlockableViewTest>().SetUnlockable(Unlockables.GetRandom());
        levels.AddProgress(0);
        levels.AddProgress(30);
        levels.AddProgress(31);
        levels.AddProgress(100);
        levels.AddProgress(100000000);

        bool _all_called = HasAllUnlockedOnce(Unlockables);
        Assert.IsTrue(_all_called, "Not All Configs have been unlocked");  

    }

    bool HasAllUnlockedOnce(List<Unlockable<BlaConfig>> configs)
    {

        foreach (var un in configs)
        {
            if (un.Item._CALLED != 1)
                return false;
        }
        return true;
    }
    int getunlock(BlaConfig c)
    {
        return c.requirement;
    }

    string getid(BlaConfig c)
    {
        return c.foo.FOO;
    }

    void OnUnlock(BlaConfig onUnlock)
    {
        onUnlock._CALLED++;
        Debug.Log("Unlocked "+onUnlock.foo.FOO+" " + onUnlock._CALLED);
    }

    Levels MockLevels()
    {
        LevelConfig Conf = new global::LevelConfig();
        Conf.RequiredProgress = new List<int> { 20, 100, 300, 1000, 5000, 99999 };
        Conf.Save = false;
        Conf.SaveID = "_LEVELS_MOCK";
        return new Levels(Conf);
    }

    List<BlaConfig> MockConfigs()
    {
        List<BlaConfig> configs = new List<BlaConfig>();
        configs.Add(new BlaConfig(0, "1"));
        configs.Add(new BlaConfig(6, "6"));
        configs.Add(new BlaConfig(0, "1_2"));
        configs.Add(new BlaConfig(3, "3"));

        return configs;
    }
}




[System.Serializable]
public class BlaConfig
{
    public int _CALLED;
    public int requirement;
    public Foo foo;

    public BlaConfig(int _requirement, string TestUnlock)
    {
        foo = new Foo(TestUnlock);
        requirement = _requirement;
    }
}


public class Foo
{
    public string FOO;
    public Foo(string f)
    {
        FOO = f;
    }
}