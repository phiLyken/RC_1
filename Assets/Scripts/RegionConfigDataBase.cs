using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionConfigDataBase : ScriptableObject {
	
	[SerializeField]
	public List<RegionPool> AllPools;

    public RegionConfig StartRegion;

	void OnEnable(){
		if(AllPools == null){
			AllPools = new List<RegionPool>();
		}
	}

    public RegionPool GetPool(int i)
    {
        return AllPools[i];
    }
 }
	
[System.Serializable]
public class RegionPool{
	[SerializeField]
	public List<WeightedRegion> Regions;

    [SerializeField]
    public List<WeightedRegion> Camps;
}