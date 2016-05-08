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

    public UnitActionBase[] Actions;

    public int Owner;
    public int TriggerRange;
    public int TurnTimeOffset;
    public GameObject Mesh;


}

public class UnitSpawnManager : MonoBehaviour {
    
    public int MinSpawn;
    public int MaxSpawn;

    List<UnitSpawner> Spawners;
    
    void Awake()
    {

        //Select all spawners that have spawnOnawke disabled
        Spawners = (from c in GetComponentsInChildren<UnitSpawner>() where !c.SpawnOnAwake select c).ToList();
      
    }

    public void SpawnUnits()
    {
        int unitCount = GetUnitSpawnCount();
        Debug.Log(gameObject.name+" Spawning Units " + unitCount);
        SpawnUnits(unitCount);
    }

    void SpawnUnits(int count)
    {
        MyMath.GetRandomObjects(Spawners, count).ForEach( spawner => spawner.SpawnUnit()) ;
    }

    int GetUnitSpawnCount()
    {
        return Mathf.Min( Random.Range(MinSpawn, MaxSpawn+1  ), Spawners.Count);
    }





   
}
