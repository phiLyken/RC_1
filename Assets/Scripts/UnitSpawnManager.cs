using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class UnitSpawnManager : MonoBehaviour {

    
    public List<Tile> SpawnTiles;

    public int MinSpawn;
    public int MaxSpawn;

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
            SpawnUnit(EnemyConfig.GetUnitPrefab(), t);
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
