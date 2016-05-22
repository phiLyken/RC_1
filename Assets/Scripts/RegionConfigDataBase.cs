using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionConfigDataBase : ScriptableObject {
	
	[SerializeField]
	private List<RegionPool> AllPools;

    public RegionConfig StartRegion;

	void OnEnable(){
		if(AllPools == null){
			AllPools = new List<RegionPool>();
		}
	}

    public static RegionConfigDataBase GetDataBase()
    {
        return Resources.Load("Regions/region_balancing_config") as RegionConfigDataBase; 
    }

    public static RegionPool GetPoolConfig(int index)
    {
        RegionConfigDataBase db = GetDataBase();

        if(index >= db.AllPools.Count)
            Debug.LogWarning("Not sufficient index for Region Pools " + index + " items in pool" + db.AllPools.Count);

        return db.AllPools[Mathf.Min(index, db.AllPools.Count - 1)];
       
    }

}
	
[System.Serializable]
public class RegionPool{
	[SerializeField]
	public List<WeightedRegion> Regions;

    [SerializeField]
    public List<WeightedRegion> Camps;
}