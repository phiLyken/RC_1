using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionConfigDataBase : ScriptableObject {

    public Sprite DifficultySprite;

    [HideInInspector]
    public string SelectionName;

    public WorldCrumbler CrumblerSetting;

    public bool IsTutorial;
 

    public int Difficulty;
 
	[SerializeField]
	public List<RegionPool> AllPools;

    [SerializeField]
    public List<WeightedRegion> Camps;

    public RegionConfig StartRegion;

	void OnEnable(){
		if(AllPools == null){
			AllPools = new List<RegionPool>();
		}
	}

    public RegionPool GetPool(int i)
    {
        if (AllPools.Count <= i)
        {
            Debug.LogWarning("NO POOL FOR LEVEL " + i);

            return null;
        }
        return AllPools[i];
    }

    public bool IsUnlocked()
    {
        return IsTutorial || PlayerPrefs.GetInt(Constants.TUTORIAL_SAVE_ID) == 1;
    }
    public bool IsCompleteInSave()
    {
        return  PlayerPrefs.GetString("_level_complete").Contains(GetInstanceID().ToString());
    }
    public void CompleteInSave()
    {
        string saved = PlayerPrefs.GetString("_level_complete");
        saved += "_" + GetInstanceID();
        PlayerPrefs.SetString("_level_complete", saved);

    }

    public void RemoveFromSave()
    {
        string saved = PlayerPrefs.GetString("_level_complete");
        string to_remove = "_" + GetInstanceID();
        int index = saved.IndexOf(to_remove);
        saved = saved.Remove(index, to_remove.Length);
        PlayerPrefs.SetString("_level_complete", saved);
    }
 }
	
[System.Serializable]
public class RegionPool{
	[SerializeField]
	public List<WeightedRegion> Regions;

 
}