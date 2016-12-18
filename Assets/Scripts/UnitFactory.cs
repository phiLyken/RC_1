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
    public static Unit CreateUnit(ScriptableUnitConfig data, int group, MyMath.R_Range initiative_range)
    {
        if (data == null)
        {
            Debug.LogWarning("cant create unit, data == null");
            return null;
        }
        Debug.Log("^unitCreating Unit " + data.ID);
        GameObject base_unit                            = Instantiate(Resources.Load("base_unit")) as GameObject;
        SpawnLootOnDeath loot                           = base_unit.AddComponent<SpawnLootOnDeath>();
        Unit_EffectManager effect_manager               = base_unit.AddComponent<Unit_EffectManager>();
        UnitStats stats                                 = MakeStats(base_unit, data, effect_manager, initiative_range);
        UnitInventory inventory                         = MakeInventory(data, base_unit, stats);
        ActionManager actions                           = MakeActions(data, base_unit);
        GameObject mesh                                 = MakeMesh(data, base_unit);
        
        UnitAnimationController animations              = mesh.AddComponent<UnitAnimationController>();
        UnitRotationController rotator                  = mesh.AddComponent<UnitRotationController>();

        UnitAdrenalineRushParticleManager adr_particles = base_unit.GetComponent<UnitAdrenalineRushParticleManager>();
        
        
        Unit m_unit                                     = base_unit.AddComponent<Unit>();
        Unit_UnitDeath unit_death                       = base_unit.AddComponent<Unit_UnitDeath>();


        adr_particles.Init(stats);
        AddActiveTurnIndicator(m_unit, data.Owner == 0);
        GetName(data, base_unit);
        GiveWeapon(data, mesh, inventory);
        
       
        loot.Init(m_unit, data.LootCategory);
        effect_manager.SetUnit(m_unit);
        animations.Init(m_unit, mesh);
       
        m_unit.OwnerID = data.Owner;



        if (data.Owner == 1)
        {
            UnitAI ai = MakeAI(data, group, base_unit, m_unit);
            rotator.Init(m_unit.GetComponent<WaypointMover>(), ai.GetLookRotation);
            ai.OnPreferredTargetChange += unit => rotator.TurnToPosition(unit.transform);
        } else
        {
            rotator.Init(m_unit.GetComponent<WaypointMover>(), delegate     { return m_unit.transform.position + m_unit.transform.forward; }    );
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
        GameObject mesh = Instantiate( Resources.Load("Units/playermodel")) as GameObject;


        SpawnSkinnedMeshToUnit(mesh, data.MeshConfig.HeadConfig.GetHead().Mesh, data.MeshConfig.Suit);

        mesh.transform.SetParent(base_unit.transform, false);
        mesh.transform.localPosition = Vector3.zero;
        mesh.transform.localScale = Vector3.one;

        return mesh;
    }

    private static void GetName(ScriptableUnitConfig data, GameObject base_unit)
    {
        string name = data.MeshConfig.Names.GetName();

        if (string.IsNullOrEmpty(name))
        {
            name = "UNKOWN " + Random.Range(0, 100);
        }

        base_unit.name = name;

    }

    private static UnitAI MakeAI(ScriptableUnitConfig data, int group, GameObject base_unit, Unit m_unit)
    {
        UnitAI ai = m_unit.gameObject.AddComponent<UnitAI>();
        ai.group_id = group;

        GameObject Cover = Instantiate(Resources.Load("enemy_unit_cover")) as GameObject;

        Cover.transform.SetParent(m_unit.transform, true);
        Cover.transform.localPosition = Vector3.zero;

        Cover.GetComponent<UnitTrigger>().SetTarget(ai);
        Cover.GetComponent<BoxCollider>().size = new Vector3(1 + 2 * data.TriggerRange, 1 + 2 * data.TriggerRange, 1 + 2 * data.TriggerRange);

        ai.Cover = Cover;

        return ai;
    }

    private static UnitInventory MakeInventory(ScriptableUnitConfig data, GameObject base_unit, UnitStats stats)
    {
       
        UnitInventory inventory = base_unit.AddComponent<UnitInventory>();
        inventory.Init(stats, data.BaseStats);
        
      


        return inventory;
 

    }
  
    public static List<GameObject> SpawnSkinnedMeshToUnit(GameObject target_unit, GameObject head, GameObject suit)
    {

        Transform target_skeleton_root = target_unit.transform.FindDeepChild("humanoid");

        List<SkinnedMeshRenderer> suitobjects = suit.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();

        suitobjects.Add(head.GetComponentsInChildren<SkinnedMeshRenderer>().First());

        List<GameObject> skinned_objects = MyMath.InsantiateObjects(suitobjects.Select(skn => skn.gameObject).ToList());


        skinned_objects.ForEach(obj => SkinnedMeshTools.AddSkinnedMeshTo(obj.gameObject, target_skeleton_root));
      //  Debug.Log("spawn skinned " + skinned_objects.Count);
        return skinned_objects;
    }

    /// <summary>
    /// http://answers.unity3d.com/questions/44355/shared-skeleton-and-animation-state.html
    /// </summary>
    /// <param name="target"></param>
    /// <param name="attachment"></param>
    public static void AttachSkinnedMesh(SkinnedMeshRenderer target, SkinnedMeshRenderer attachment)
    {
       
        SkinnedMeshRenderer targetRenderer = target;
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in targetRenderer.bones)
        {
            boneMap[bone.name] = bone;
        }

          
        Transform[] boneArray = targetRenderer.bones;
        for (int idx = 0; idx < boneArray.Length; ++idx)
        {
            string boneName = boneArray[idx].name;
            if (false == boneMap.TryGetValue(boneName, out boneArray[idx]))
            {
                Debug.LogError("failed to get bone: " + boneName);
                Debug.Break();
            }
        }
        attachment.bones = boneArray; //take effect
        
    }

    public static void GiveWeapon(ScriptableUnitConfig data, GameObject unit_mesh, UnitInventory inventory)
    {
        Weapon weapon = SpawnWeaponToUnit(unit_mesh, data.Weapon);


        weapon.transform.SetParent(unit_mesh.transform);
       
        inventory.AddItem(weapon, 1);
    }

    public static UnitAnimation MakeUnitAnimations(GameObject unit_mesh, WeaponMesh weapon, int index, AnimationCallbackCaster callbacks, GetBool b )
    {
        WeaponAnimator animator_right = new WeaponAnimator(weapon.AttachmentRight, weapon.FX_Right, weapon.MuzzleRight);
        WeaponAnimator animator_left = new WeaponAnimator(weapon.AttachmentLeft, weapon.FX_Left, weapon.MuzzleLeft);
        Animator unit_animator = unit_mesh.GetComponent<Animator>();
        UnitAnimation_IdleController idle = unit_mesh.GetComponent<UnitAnimation_IdleController>();

        return new UnitAnimation().Init(unit_animator, animator_right, animator_left, index, callbacks, idle.GetId, b );
    }

    public static WeaponMesh SpawnWeaponMeshToUnit(GameObject unit_mesh, WeaponMesh weapon_mesh_prefab)
    {
        WeaponMesh weapon_mesh = Instantiate(weapon_mesh_prefab.gameObject).GetComponent<WeaponMesh>();

        weapon_mesh.transform.SetParent(unit_mesh.transform);

        if (weapon_mesh.AttachmentLeft != null)
           UnitMesh_Attachment.AttachToBone(unit_mesh, weapon_mesh.AttachmentLeft, AttachmentPoints.left_hand);

        if (weapon_mesh.AttachmentRight != null)
           UnitMesh_Attachment.AttachToBone(unit_mesh, weapon_mesh.AttachmentRight, AttachmentPoints.right_hand);

        return weapon_mesh;
    }

    public static Weapon SpawnWeaponToUnit(GameObject unit_mesh, Weapon weapon)
    {
      

        Weapon instance = Instantiate(weapon.gameObject).GetComponent<Weapon>();
        instance.gameObject.transform.SetParent(unit_mesh.transform);
        instance.WeaponMesh =  SpawnWeaponMeshToUnit(unit_mesh, weapon.WeaponMesh);
        return instance;   

       
    }

    static UnitStats MakeStats(GameObject target, ScriptableUnitConfig conf, Unit_EffectManager effects, MyMath.R_Range range )
    {

        UnitStats stats = target.AddComponent<UnitStats>();

        conf.BaseStats.StartTurnTime = range;

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

    public static void AddActiveTurnIndicator(Unit u, bool friendly)
    {
        string path = "Highlights/highlighter_" +( friendly ? "friendly" : "hostile");

        GameObject go = Instantiate(Resources.Load(path)) as GameObject;

        ToggleActiveOnTurn toggle = go.gameObject.AddComponent<ToggleActiveOnTurn>();
        toggle.SetUnit(u);
    }
}