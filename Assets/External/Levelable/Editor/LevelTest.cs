using UnityEngine;
using System.Collections;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class LevelTest  {


    [Test]
    public void LevelOnAmountEqual()
    {
        Levels m_levels = new Levels(MockConfig());
        m_levels.AddProgress(200);

        Assert.AreEqual(2, m_levels.GetCurrentLevel(), "NOT LEVEL 2 AFTER GIVING EQUAL AMOUNT TO REQUIREMENT");
    }

    [Test]
    public void LevelCreation()
    {
        Levels m_levels = new Levels(MockConfigSaved());
        Assert.AreEqual(1, m_levels.GetCurrentLevel(), "NOT LEVEL ONE ON NEW LEVEL");

    }

    [Test]
    public void GoToMaxLevel()
    {
        Levels m_levels = new Levels(MockConfig());
        m_levels.AddProgress(m_levels.GetRequiredForLevel(m_levels.GetMaxLevel()));
        Assert.AreEqual(7, m_levels.GetCurrentLevel(), "LEVEL NOT MAX LEVEL");
    }

    [Test]
    public void GoToNextLevel()
    {
        Levels m_levels = new Levels(MockConfig());
        m_levels.AddProgress(1000 );
        int nextlevel = m_levels.GetCurrentLevel() + 1;
        int progress_to_add = (m_levels.GetRequiredForLevel(nextlevel) - m_levels.GetProgress());
        m_levels.AddProgress(progress_to_add);


        Assert.AreEqual(nextlevel, m_levels.GetCurrentLevel(), "LEVEL NOT NEXT LEVEL");
    }

    [Test]
    public void LevelSaveTest()
    {
        LevelConfig mockConfig = MockConfigSaved();

        Levels m_levels = new Levels(mockConfig);
        

        m_levels.AddProgress(100000000);
        int level = m_levels.GetCurrentLevel();

        Levels m_levels_2 = new Levels(mockConfig);


        Assert.AreEqual(level, m_levels_2.GetCurrentLevel(), "SAVED LEVEL NOT NEW LEVEL");
    }

    public static LevelConfig MockConfigSaved()
    {
        string SAVEID = "_LEVEL_TEST";

        LevelConfig conf = MockConfig();
        conf.SaveID = SAVEID;
        conf.Save = true;
        PlayerPrefs.SetInt(SAVEID, 0);
        return conf;
    }

    public static LevelConfig MockConfig()
    {
        LevelConfig conf = new LevelConfig();
        conf.RequiredProgress = new List<int>() { 200, 1000, 3000, 6000, 100000, 2000000 };
        conf.Save = false;
        return conf;
    }

}
