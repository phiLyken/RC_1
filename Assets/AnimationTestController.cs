using UnityEngine;
using System.Collections;

public class AnimationTestController : MonoBehaviour
{

    UnitAnimation m_Animator;
    WeaponMesh current;

    public GameObject TargetUnit;

    public WeaponMesh[] TestWeapons;


    void Awake()
    {
        Init(TestWeapons[0]);
    }

    public void Init(WeaponMesh mesh)
    {
    

        if (current != null)
        {
            Destroy(current.AttachmentLeft);
            Destroy(current.AttachmentRight);
            Destroy(current.gameObject);
        }

        /*
        if(m_Animator != null)
        {
            caster.OnAbilityTrigger -= m_Animator.AbilityCallback;
            caster.OnWeaponHide -= m_Animator.WeaponHide;
            caster.OnWeaponShow -= m_Animator.WeaponShow;
        }*/
        AnimationCallbackCaster caster = TargetUnit.GetComponent<AnimationCallbackCaster>();

        current = UnitFactory.SpawnWeaponMeshToUnit(TargetUnit, mesh);
        m_Animator = UnitFactory.MakeUnitAnimations(TargetUnit, current, current.WeaponIndex, caster);


    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(10, 10, 500, 500));
        GUI.BeginGroup(new Rect(0, 0, 350, 500), "ONE SHOTS");

        if (GUI.Button(new Rect(0, 30, 100, 25), "GET HIT"))
            m_Animator.SetTrigger(UnitAnimationTypes.bHit.ToString() );
        if (GUI.Button(new Rect(0, 60, 100, 25), "RAGE"))
            m_Animator.SetTrigger(UnitAnimationTypes.bRage.ToString());
        if (GUI.Button(new Rect(0, 90, 100, 25), "LOOT"))
            m_Animator.SetTrigger(UnitAnimationTypes.bLooting.ToString());
        if (GUI.Button(new Rect(0, 120, 100, 25), "HEAL"))
            m_Animator.SetTrigger(UnitAnimationTypes.bHealing.ToString());
        if (GUI.Button(new Rect(0, 150, 100, 25), "SHOOT"))
            m_Animator.SetTrigger(UnitAnimationTypes.bShooting.ToString());
        if (GUI.Button(new Rect(0, 180, 100, 25), "INT ATTACK"))
            m_Animator.SetTrigger(UnitAnimationTypes.bIntAttack.ToString());
        if (GUI.Button(new Rect(0, 210, 100, 25), "DIE"))
            m_Animator.SetTrigger(UnitAnimationTypes.bDying.ToString());

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, 350, 350, 200), "OTHER STATES");

        if (GUI.Button(new Rect(0, 30, 100, 25), "MOVE: True"))
        {
            m_Animator.SetWalking(true);
        }

        if (GUI.Button(new Rect(0, 60, 100, 25), "MOVE: False"))
        {
            m_Animator.SetWalking(false);
        }

        GUI.EndGroup();

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(Screen.width - 200, 0, 350, 500), "WEAPON STATES");

        if (GUI.Button(new Rect(0, 30, 100, 25), "CCW"))
        {
            m_Animator.SetWeaponIndex(0);
        }
        if (GUI.Button(new Rect(0, 60, 100, 25), "CCWnShield"))
        {
            m_Animator.SetWeaponIndex(1);
        }
        if (GUI.Button(new Rect(0, 90, 100, 25), "Pistol"))
        {
            m_Animator.SetWeaponIndex(2);
        }
        if (GUI.Button(new Rect(0, 120, 100, 25), "Dual Pistol"))
        {
            m_Animator.SetWeaponIndex(3);
        }
        if (GUI.Button(new Rect(0, 150, 100, 25), "Rifle"))
        {
            m_Animator.SetWeaponIndex(4);
        }
        if (GUI.Button(new Rect(0, 180, 100, 25), "BFG"))
        {
            m_Animator.SetWeaponIndex(5);
        }
        if (GUI.Button(new Rect(0, 210, 100, 25), "Greatweapon"))
        {
            m_Animator.SetWeaponIndex(6);
        }

        GUI.EndGroup();
        GUI.BeginGroup(new Rect(Screen.width - 200, Screen.height-250, 350, 500), "WEAPON MESHES");

        for(int i = 0; i <= TestWeapons.Length-1; i++)
        {
            if (GUI.Button(new Rect(0, 30 + i*30, 100, 25), "wpn mesh "+i))
            {
                Init(TestWeapons[i]);
            }
        }

        GUI.EndGroup();


    }



}
