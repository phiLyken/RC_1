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
        Spawners = ( GetComponentsInChildren<UnitSpawner>()).ToList();        
    }

    public void SpawnUnits()
    {
        Spawners.ForEach(s => {         
            if (s.SpawnOnAwake)
            {               
                s.SpawnUnit();
            }
        });
             
     
        SpawnUnits(GetUnitSpawnCount());
    }

    void SpawnUnits(int count)
    {
        
        MyMath.GetRandomObjects(Spawners.Where(sp => !sp.SpawnOnAwake ).ToList() , count).ForEach( spawner => spawner.SpawnUnit()) ;
    }

    int GetUnitSpawnCount()
    {
        return Mathf.Min( Random.Range(MinSpawn, MaxSpawn+1  ), Spawners.Count);
    }





   
}
