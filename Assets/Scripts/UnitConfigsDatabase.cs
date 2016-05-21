using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UnitConfigsDatabase : ScriptableObject {

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

    public static List<UnitConfig> GetAllConfigs()
    {
        UnitConfigsDatabase db = Resources.Load("Units/unit_configs") as UnitConfigsDatabase;
        return db.configs;
    }
	public static UnitConfig ___GetConfig(string id){
        UnitConfigsDatabase db = Resources.Load("Units/unit_configs") as UnitConfigsDatabase;
        foreach (UnitConfig c in db.configs) if(c.ID == id) return c;

		Debug.LogWarning("CONFIG NOT FOUND "+id);
		return null;
	}

    public static List<UnitConfig> GetConfigForOwner(int id)
    {
        List<UnitConfig> ret = new List<UnitConfig>();

        UnitConfigsDatabase db = Resources.Load("unit_stats") as UnitConfigsDatabase;
        foreach (UnitConfig c in db.configs) if (c.Owner == id) ret.Add(c);
        return ret;
    }

}
