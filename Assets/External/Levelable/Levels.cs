using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Levels {

    public event Action<int> OnLevelUp;
    public event Action<int> OnProgress;

    public LevelConfig config;

    public Levels(LevelConfig _config)
    {
      
        config = _config;
        currentProgress = config.GetSavedProgress();
        cachedLevel = config.GetLevelForProgress(currentProgress);
    }

    int cachedLevel;

    int currentProgress;
 
    public int GetProgress()
    {
        return currentProgress;
    }

    public int GetMaxLevel()
    {
        return config.RequiredProgress.Count + 1;
    }
    public float GetProgressInLevel()
    {
        return M_Math.GetPercentpointsOfValueInRange(currentProgress, config.GetProgressForLevel(cachedLevel), config.GetProgressForLevel(cachedLevel + 1));
  
    }
    public int GetRequiredForLevel(int level)
    {
        return config.RequiredProgress[Mathf.Clamp(level-2, 0, config.RequiredProgress.Count - 1)];
    }
    public void AddProgress(int amount)
    {        
        currentProgress += amount;
        config.UpdateProgress(currentProgress);
        int current_level = GetCurrentLevel();

        OnProgress.AttemptCall(currentProgress);

        if (cachedLevel != current_level)
        {
            cachedLevel = current_level;
            OnLevelUp.AttemptCall(current_level);
        }
    }

    public int GetCurrentLevel()
    {
        return config.GetLevelForProgress(currentProgress);
    }

}

[System.Serializable]
public class LevelConfig
{
    public bool Save;
    public string SaveID;
    public List<int> RequiredProgress;

    public int GetSavedProgress()
    {
        return (Save && PlayerPrefs.HasKey(SaveID)) ? PlayerPrefs.GetInt(SaveID) : 0;
    }

    public int GetLevelForProgress(int progress)
    {
   
        for(int i = 0; i<RequiredProgress.Count; i++)
        {

            if(RequiredProgress[i] > progress)
            {
                return i+1;
            }
           
        }

        return RequiredProgress.Count + 1;
        
    }

    public int GetProgressForLevel(int level)
    {
        if (level <= 1)
            return 0;

        return RequiredProgress[Mathf.Min(level-2, RequiredProgress.Count - 1)];
    }

    public  void UpdateProgress(int _bew)
    {
        if(Save)
          PlayerPrefs.SetInt(SaveID, _bew);
    }

}