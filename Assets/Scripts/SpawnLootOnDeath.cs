using UnityEngine;
using System.Collections;

public class SpawnLootOnDeath : MonoBehaviour {

    Unit m_unit;
    EnemyDropCategory cat;

    public void Init(Unit u, EnemyDropCategory category)
    {
        cat = category;
        m_unit = u;
        Unit.OnUnitKilled += OnDeath;
    }

    void OnDeath(Unit u)
    {
        if(u == m_unit)
        {
            Tile_Loot.AddLoot(u.currentTile, cat);
        }

    }
}
