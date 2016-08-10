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

        GameObject base_unit                = Instantiate(Resources.Load("base_unit")) as GameObject;

        Unit_EffectManager effect_manager   = base_unit.AddComponent<Unit_EffectManager>();
        UnitStats stats                     = MakeStats(base_unit, data, effect_manager, turntime);
        UnitInventory inventory             = MakeInventory(data, base_unit, stats);
        ActionManager actions               = MakeActions(data, base_unit);
        GameObject mesh                     = MakeMesh(data, base_unit);

        GetName(data, base_unit);
       

    
        AttachWeapon(data, mesh, inventory);

        Unit m_unit = base_unit.AddComponent<Unit>();

        effect_manager.SetUnit(m_unit);

        m_unit.OwnerID = data.Owner;      
        
        if (data.Owner == 1)
        {
            MakeAI(data, group, base_unit, m_unit);
        }    

        return m_unit;
    }

    private static ActionManager MakeActions(ScriptableUnitConfig data, GameObject base_unit)
    {
        UnitActionBase[] Actions = MyMath.SpawnFromList(data.Actions.ToList()).ToArray();
        MyMath.SetListAsChild(Actions.ToList(), base_unit.transform);
        return   base_unit.AddComponent<ActionManager>();
    }

    private static GameObject MakeMesh(ScriptableUnitConfig data, GameObject base_unit)
    {
        GameObject mesh = Instantiate(data.Mesh);
        mesh.transform.SetParent(base_unit.transform, false);
        mesh.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        mesh.transform.localScale = Vector3.one;


        return mesh;
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

        Cover.GetComponent<UnitTrigger>().SetTarget(ai);
        Cover.GetComponent<BoxCollider>().size = new Vector3(1 + 2 * data.TriggerRange, 1 + 2 * data.TriggerRange, 1 + 2 * data.TriggerRange);

        ai.Cover = Cover;
    }

    private static UnitInventory MakeInventory(ScriptableUnitConfig data, GameObject base_unit, UnitStats stats)
    {

        UnitInventory inventory = base_unit.AddComponent<UnitInventory>();
        inventory.Init(stats);



        return inventory;
       // Armor armor = Instantiate(data.Armor);
       // armor.transform.SetParent(base_unit.transform);
     //   inventory.AddItem(armor,1);

    }

    private static void AttachArmor(ArmorConfig config, GameObject unit_mesh)
    {
  
    }


    private static void AttachWeapon(ScriptableUnitConfig data, GameObject unit_mesh, UnitInventory inventory)
    {
        Weapon weapon = Instantiate(data.Weapon);
        weapon.transform.SetParent(unit_mesh.transform);
        inventory.AddItem(weapon, 1);
    }
    static UnitStats MakeStats(GameObject target, ScriptableUnitConfig conf, Unit_EffectManager effects, int start_initiative)
    {

        UnitStats stats = target.AddComponent<UnitStats>();
        stats.Init(StatsHelper.GetStatListForInit(conf.BaseStats), effects);

        return stats;
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