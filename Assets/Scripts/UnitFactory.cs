using UnityEngine;
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
    public static Unit CreateUnit(ScriptableUnitConfig data, int group, int turntime)
    {
        if (data == null)
        {
            Debug.LogWarning("cant create unit, data == null");
            return null;
        }
        GameObject base_unit = Instantiate(Resources.Load("base_unit")) as GameObject;

        GetName(data, base_unit);

        MakeActions(data, base_unit);

        MakeMesh(data, base_unit);

        //inventory should be created before stats
        MakeInventory(data, base_unit);       

        Unit_EffectManager  effect_manager = base_unit.AddComponent<Unit_EffectManager>();
        MakeStats(base_unit, data);
        Unit m_unit = base_unit.AddComponent<Unit>();
        effect_manager.SetUnit(m_unit);

        m_unit.OwnerID = data.Owner;
        m_unit.TurnTime = turntime;
        
        if (data.Owner == 1)
        {
            MakeAI(data, group, base_unit, m_unit);
        }
      

        // Debug.Log("Created unit " + m_unit.gameObject.name);
        return m_unit;
    }

    private static void MakeActions(ScriptableUnitConfig data, GameObject base_unit)
    {
        UnitActionBase[] Actions = MyMath.SpawnFromList(data.Actions.ToList()).ToArray();
        MyMath.SetListAsChild(Actions.ToList(), base_unit.transform);
        base_unit.AddComponent<ActionManager>();
    }

    private static void MakeMesh(ScriptableUnitConfig data, GameObject base_unit)
    {
        GameObject mesh = Instantiate(data.Mesh);
        mesh.transform.SetParent(base_unit.transform, false);
        mesh.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        mesh.transform.localScale = Vector3.one;
    }

    private static void GetName(ScriptableUnitConfig data, GameObject base_unit)
    {
        string name = MyMath.GetRandomObjectAndRemove(Constants.names);

        if (string.IsNullOrEmpty(name))
        {
            name = "UNKOWN " + Random.Range(0, 100);
        }

        base_unit.name = (data.Owner == 0 ? "OFFICER " : "INMATE ") + name;
    }

    private static void MakeAI(ScriptableUnitConfig data, int group, GameObject base_unit, Unit m_unit)
    {
        UnitAI ai = m_unit.gameObject.AddComponent<UnitAI>();
        ai.group_id = group;

        GameObject Cover = Instantiate(Resources.Load("enemy_unit_cover")) as GameObject;
        Cover.transform.SetParent(m_unit.transform, true);
        Cover.transform.localPosition = Vector3.zero;

        Cover.GetComponent<UnitTrigger>().Target = base_unit.gameObject;
        Cover.GetComponent<BoxCollider>().size = new Vector3(1 + 2 * data.TriggerRange, 1 + 2 * data.TriggerRange, 1 + 2 * data.TriggerRange);

        ai.Cover = Cover;
    }

    private static void MakeInventory(ScriptableUnitConfig data, GameObject base_unit)
    {
        UnitInventory inventory = base_unit.AddComponent<UnitInventory>();

        WeaponConfig weapon = Instantiate(data.Weapon);
        weapon.transform.SetParent(base_unit.transform);
        inventory.AddItem(  weapon );
        inventory.EquipedWeapon = inventory.GetItem( ItemTypes.weapon ) as WeaponConfig;

        ArmorConfig armor = Instantiate(data.Armor);
        armor.transform.SetParent(base_unit.transform);
        inventory.AddItem(armor);
        inventory.EquipedArmor = inventory.GetItem( ItemTypes.armor ) as ArmorConfig;
    }

    static void MakeStats(GameObject target, ScriptableUnitConfig conf)
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
        
        u.SetTile(tile, true);
    }
}