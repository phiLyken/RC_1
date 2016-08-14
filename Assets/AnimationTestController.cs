using UnityEngine;
using System.Collections;

public class AnimationTestController : MonoBehaviour
{

    UnitAnimation m_Animator;
    WeaponMesh current;

    public WeaponMesh[] TestWeapons;


    void Awake()
    {

        Init(TestWeapons[0]);
    }

    public void Init(WeaponMesh mesh)
    {
        if(current != null)
        {
            Destroy(current.AttachmentLeft);
            Destroy(current.AttachmentRight);
            Destroy(current.gameObject);
        }

        current = UnitFactory.SpawnWeaponMeshToUnit(gameObject, mesh);
        m_Animator = UnitFactory.MakeUnitAnimations(gameObject, current, current.WeaponIndex);
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(10, 10, 500, 500));
        GUI.BeginGroup(new Rect(0, 0, 350, 500), "ONE SHOTS");

        if (GUI.Button(new Rect(0, 30, 100, 25), "GET HIT"))
            m_Animator.SetTrigger("bHit");
        if (GUI.Button(new Rect(0, 60, 100, 25), "RAGE"))
            m_Animator.SetTrigger("bRage");
        if (GUI.Button(new Rect(0, 90, 100, 25), "LOOT"))
            m_Animator.SetTrigger("bLooting");
        if (GUI.Button(new Rect(0, 120, 100, 25), "HEAL"))
            m_Animator.SetTrigger("bHealing");
        if (GUI.Button(new Rect(0, 150, 100, 25), "SHOOT"))
            m_Animator.SetTrigger("bShooting");
        if (GUI.Button(new Rect(0, 180, 100, 25), "INT ATTACK"))
            m_Animator.SetTrigger("bIntAttack");
        if (GUI.Button(new Rect(0, 210, 100, 25), "DIE"))
            m_Animator.SetTrigger("bDying");

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
