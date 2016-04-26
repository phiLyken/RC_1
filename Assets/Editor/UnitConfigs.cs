using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UnitConfig {

	public UnitConfig(string id){
		ID = id;
	}
	public string ID;
}
public class UnitConfigs : ScriptableObject {

	[SerializeField]
	private List<UnitConfig> configs;

	void OnEnable(){
		if(configs == null){
			configs = new List<UnitConfig>();
		}
	}

	public void Add(UnitConfig conf){
		configs.Add(conf);
	}

	public void RemoveAt(int index){
		configs.RemoveAt(index);
	}

	public int COUNT{
		get { return configs.Count; }
	}

	public UnitConfig GetConfigAt(int index){
		return configs[index];
	}

	public UnitConfig GetConfig(string id){
		foreach(UnitConfig c in configs) if(c.ID == id) return c;

		Debug.LogWarning("CONFIG NOT FOUND "+id);
		return null;
	}
}
