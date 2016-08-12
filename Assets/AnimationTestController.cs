using UnityEngine;
using System.Collections;

public class AnimationTestController : MonoBehaviour
{

    Animator Anim;


    void Awake()
    {
        Anim = GetComponent<Animator>();

    }


    void OnGUI()
    {
        GUI.BeginGroup(new Rect(10, 10, 500, 500));
        GUI.BeginGroup(new Rect(0, 0, 350, 500), "ONE SHOTS");

        if (GUI.Button(new Rect(0, 30, 100, 25), "GET HIT"))
            Anim.SetTrigger("bHit");
        if (GUI.Button(new Rect(0, 60, 100, 25), "RAGE"))
            Anim.SetTrigger("bRage");
        if (GUI.Button(new Rect(0, 90, 100, 25), "LOOT"))
            Anim.SetTrigger("bLooting");
        if (GUI.Button(new Rect(0, 120, 100, 25), "HEAL"))
            Anim.SetTrigger("bHealing");
        if (GUI.Button(new Rect(0, 150, 100, 25), "SHOOT"))
            Anim.SetTrigger("bShooting");
        if (GUI.Button(new Rect(0, 180, 100, 25), "INT ATTACK"))
            Anim.SetTrigger("bIntAttack");
        if (GUI.Button(new Rect(0, 210, 100, 25), "DIE"))
            Anim.SetTrigger("bDying");

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, 350, 350, 200), "OTHER STATES");
        Anim.SetBool("bMoving", GUI.Toggle(new Rect(0, 30, 100, 25), Anim.GetBool("bMoving"), "Moving"));

        GUI.EndGroup();

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(Screen.width - 200, 0, 350, 500), "WEAPON STATES");

        if (GUI.Button(new Rect(0, 30, 100, 25), "CCW"))
        {
            Anim.SetFloat("WeaponIndex", 0);
        }
        if (GUI.Button(new Rect(0, 60, 100, 25), "CCWnShield"))
        {
            Anim.SetFloat("WeaponIndex", 1);
        }
        if (GUI.Button(new Rect(0, 90, 100, 25), "Pistol"))
        {
            Anim.SetFloat("WeaponIndex", 2);
        }
        if (GUI.Button(new Rect(0, 120, 100, 25), "Dual Pistol"))
        {
            Anim.SetFloat("WeaponIndex", 3);
        }
        if (GUI.Button(new Rect(0, 150, 100, 25), "Rifle"))
        {
            Anim.SetFloat("WeaponIndex", 4);
        }
        if (GUI.Button(new Rect(0, 180, 100, 25), "BFG"))
        {
            Anim.SetFloat("WeaponIndex", 5);
        }
        if (GUI.Button(new Rect(0, 210, 100, 25), "Greatweapon"))
        {
            Anim.SetFloat("WeaponIndex", 6);
        }






        /*      for (int i = 0; i < 7; i++)
              {
                  if (GUI.Button(new Rect(0, i * 30, 150, 25), "set weaponindex " + i))
                  {
                      Anim.SetFloat("WeaponIndex", i);
                  }

              }
      */
        GUI.EndGroup();


    }
}
