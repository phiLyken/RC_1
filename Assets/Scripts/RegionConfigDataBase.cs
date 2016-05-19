using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegionConfigDataBase : ScriptableObject {
	
	[SerializeField]
	private List<RegionBalancingConfig> configs;


	void OnEnable(){
		if(configs == null){
			configs = new List<RegionBalancingConfig>();
		}
	}

		
}
	
[System.Serializable]
public class RegionBalancingConfig{
	[SerializeField]
	private List<RegionConfig> configs;
}