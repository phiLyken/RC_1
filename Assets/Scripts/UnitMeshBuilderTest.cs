using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMeshBuilderTest : MonoBehaviour {

    List<GameObject> current;
    public GameObject Target_Unit;
    public UnitMeshConfig MeshConfig;
    public WeaponMesh Weapon;
    UnitAnimation animator;

    void Start()
    {
        Init(MeshConfig);
    }

    public void Init(UnitMeshConfig conf)
    {
       if(current != null)
        {
        //   List<GameObject> to_remove = new List<GameObject>();

            foreach(var rend in current)
            {
                Destroy(rend.gameObject);             
            }          
        }
        AnimationCallbackCaster caster = Target_Unit.GetComponent<AnimationCallbackCaster>();
        current = UnitFactory.SpawnSkinnedMeshToUnit(Target_Unit, MeshConfig.HeadConfig.GetHead().Mesh, MeshConfig.Suit);
         var wpn =  UnitFactory.SpawnWeaponMeshToUnit(Target_Unit, Weapon);
       var m_Animator = UnitFactory.MakeUnitAnimations(Target_Unit, wpn, wpn.WeaponIndex, caster, () => { return false; });
    }
}


