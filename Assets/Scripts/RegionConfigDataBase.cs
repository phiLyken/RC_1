using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionConfigDataBase : ScriptableObject {

    public AudioClip DefaultMusic;
    public AudioClip ActionMusic1;
    public AudioClip AmbientSound;

    public string SAVEID;
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
        return PlayerPrefs.HasKey(SAVEID);
    }
    public void CompleteInSave()
    {
        PlayerPrefs.SetString(SAVEID, "saved");
    
  
        PlayerPrefs.SetString(SAVEID, "saved");

    }

    public void RemoveFromSave()
    {
  
        
        PlayerPrefs.DeleteKey(SAVEID);
    }
 }
	
[System.Serializable]
public class RegionPool{
	[SerializeField]
	public List<WeightedRegion> Regions;

 
}