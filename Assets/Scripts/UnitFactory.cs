﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitFactory : MonoBehaviour
{

    /// <summary>
    /// Returns the instance of a unit
    /// </summary>
    /// <param name="data">Unit Config to determine unit type, stats, actions</param>
    /// <returns></returns>
    public static Unit CreateUnit(UnitConfig data)
    {
        GameObject base_unit = Instantiate(Resources.Load("base_unit")) as GameObject;
        base_unit.name = data.ID;

        UnitActionBase[] Actions = MyMath.SpawnFromList(data.Actions.ToList()).ToArray();
        MyMath.SetListAsChild(Actions.ToList(), base_unit.transform);
        base_unit.AddComponent<ActionManager>();

        GameObject mesh = Instantiate(data.Mesh);
        mesh.transform.SetParent(base_unit.transform, false);
        mesh.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        mesh.transform.localScale = Vector3.one;

        addStats(base_unit, data);

        Unit m_unit = base_unit.AddComponent<Unit>();
        m_unit.OwnerID = data.Owner;

        if(data.Owner == 1)
        {
            m_unit.gameObject.AddComponent<UnitAI>();
        }
        return m_unit;
    }

    static void addStats(GameObject target, UnitConfig conf)
    {
        UnitStats stats;
        if (conf.StatType == StatType.simple)
        {
            stats = target.AddComponent<EnemyUnitStats>();
        }
        else
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

    /// <summary>
    /// Puts the unit into the game world.
    /// </summary>
    /// <param name="u"></param>
    /// <param name="tile"></param>
    public static void SpawnUnit(Unit u, Tile tile)
    {
       // Unit newUnit = (Instantiate(u.gameObject) as GameObject).GetComponent<Unit>();
        u.SetTile(tile, true);
    }
}