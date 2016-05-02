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
    public StatType StatType;

    public StatInfo[] stats;

    public string ID;
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

    
    public List<Tile> SpawnTiles;

    public int MinSpawn;
    public int MaxSpawn;

    public static Unit CreateUnit(UnitConfig data)
    {
        GameObject base_unit = Instantiate(Resources.Load("base_unit")) as GameObject;
        UnitActionBase[] Actions = MyMath.SpawnFromList(data.Actions.ToList()).ToArray() ;
        MyMath.SetListAsChild(Actions.ToList(), base_unit.transform);
        base_unit.AddComponent<ActionManager>();

        GameObject mesh = Instantiate(data.Mesh);
        mesh.transform.SetParent(base_unit.transform, false);
        mesh.transform.localPosition = Vector3.zero + Vector3.up *0.5f;
        mesh.transform.localScale = Vector3.one;

        addStats(base_unit, data);

        Unit m_unit = base_unit.AddComponent<Unit>();
        return m_unit;
    }

    static void addStats(GameObject target, UnitConfig conf)
    {
        UnitStats stats;
        if (conf.StatType == StatType.simple)
        {
            stats = target.AddComponent<EnemyUnitStats>();            
        } else
        {
            stats = target.AddComponent<PlayerUnitStats>();
           
        }
        stats.Stats = new StatInfo[conf.stats.Length];
        for (int i = 0; i < stats.Stats.Length; i++)
        {
            StatInfo inf = new StatInfo();
            inf.Stat = conf.stats[i].Stat;
            inf.Amount = conf.stats[i].Amount;
            stats.Stats[i] = inf;
        }
    }

    public  void SpawnUnits()
    {
        int unitCount = GetUnitSpawnCount();
        Debug.Log(gameObject.name+" Spawning Units " + unitCount);
        SpawnUnits(GetUnitSpawnCount());
    }

    void SpawnUnits(int count)
    {       
        List<Tile> tilesToSpawn = MyMath.GetRandomObjects(SpawnTiles, count);
        foreach(Tile t in tilesToSpawn)
        {
            SpawnUnit(EnemyConfigs.GetUnitPrefab(), t);
        }
    }

    int GetUnitSpawnCount()
    {
        return Mathf.Min( Random.Range(MinSpawn, MaxSpawn+1  ), SpawnTiles.Count);
    }

    void SpawnUnit(Unit u, Tile tile)
    {
        Unit newUnit = (Instantiate(u.gameObject) as GameObject).GetComponent<Unit>();
        newUnit.SetTile(tile, true);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red * 0.8f;
        foreach(Tile t in SpawnTiles)
        {
            Gizmos.DrawCube(t.GetPosition(), Vector3.one);
        }
    }
}
