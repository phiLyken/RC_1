using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public enum StatType
{
    complex,simple
}
[System.Serializable]
public class UnitConfig
{
    public string ID;
    public StatType StatType;
    public StatInfo[] stats;   
    public int Owner;
    public UnitActionBase[] Actions;
   
    public int TurnTimeOffset;
    public GameObject Mesh;
    public UnitConfig(string id)
    {
        ID = id;
    }

}

public class UnitSpawnManager : MonoBehaviour {
    
    public int MinSpawn;
    public int MaxSpawn;

    List<UnitSpawner> Spawners;
    
    void Awake()
    {
        Spawners = GetComponentsInChildren<UnitSpawner>().ToList();
    }

    public void SpawnUnits()
    {
        int unitCount = GetUnitSpawnCount();
        Debug.Log(gameObject.name+" Spawning Units " + unitCount);
        SpawnUnits(GetUnitSpawnCount());
    }

    void SpawnUnits(int count)
    {
        List<UnitSpawner> tilesToSpawn = MyMath.GetRandomObjects(Spawners, count);

        foreach(var t in tilesToSpawn)
        {         
           t.SpawnUnit();
        }
    }

    int GetUnitSpawnCount()
    {
        return Mathf.Min( Random.Range(MinSpawn, MaxSpawn+1  ), Spawners.Count);
    }





   
}
