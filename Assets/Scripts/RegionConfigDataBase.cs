﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionConfigDataBase : ScriptableObject {

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
 }
	
[System.Serializable]
public class RegionPool{
	[SerializeField]
	public List<WeightedRegion> Regions;

 
}