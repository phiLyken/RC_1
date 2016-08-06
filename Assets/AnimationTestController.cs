using UnityEngine;
using System.Collections;

public class AnimationTestController : MonoBehaviour {

    Animator Anim;
    
    
    void Awake()
    {
        Anim = GetComponent<Animator>();

    }	

      
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(10, 10, 500, 500));
            GUI.BeginGroup(new Rect(0, 0, 350, 500), "ONE SHOTS");
     
                if (GUI.Button(new Rect(0,30,100, 25),"GET HIT")) Anim.SetTrigger("bHit");
                if (GUI.Button(new Rect(0, 60, 100, 25), "RAGE")) Anim.SetTrigger("bRage");
                if (GUI.Button(new Rect(0, 90, 100, 25), "LOOT")) Anim.SetTrigger("bLooting");
                if (GUI.Button(new Rect(0, 120, 100, 25), "HEAL")) Anim.SetTrigger("bHealing");
                if (GUI.Button(new Rect(0, 150, 100, 25), "SHOOT")) Anim.SetTrigger("bShooting");
                if (GUI.Button(new Rect(0, 180, 100, 25), "INT ATTACK")) Anim.SetTrigger("bIntAttack");
                if (GUI.Button(new Rect(0, 210, 100, 25), "DIE")) Anim.SetTrigger("bDying");

            GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, 350, 350, 200), "OTHER STATES");
                 Anim.SetBool("bMoving", GUI.Toggle(new Rect(0, 30, 100, 25), Anim.GetBool("bMoving"), "Moving"));
        GUI.EndGroup();

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(Screen.width - 200, 0, 350, 500), "WEAPON STATES");
        Anim.SetBool("wpnPistol", GUI.Toggle(new Rect(0, 30, 150, 25), Anim.GetBool("wpnPistol"), "WEAPON PIOSTOL"));
        Anim.SetBool("wpnDualPistol", GUI.Toggle(new Rect(0, 60, 150, 25), Anim.GetBool("wpnDualPistol"), "WEAPON DUAL P"));
        Anim.SetBool("wpnRifle", GUI.Toggle(new Rect(0, 90, 150, 25), Anim.GetBool("wpnRifle"), "WPN RIFLE"));
        Anim.SetBool("wpnCCW", GUI.Toggle(new Rect(0, 120, 150, 25), Anim.GetBool("wpnCCW"), "WPN CLOSE COMBAT"));

        Anim.SetBool("wpnCCWnS", GUI.Toggle(new Rect(0, 150, 150, 25), Anim.GetBool("wpnCCWnS"), "WPN CLOSE COMBAT SHIELD"));
        Anim.SetBool("wpnGreatweapon", GUI.Toggle(new Rect(0, 180, 150, 25), Anim.GetBool("wpnGreatweapon"), "GREAT WEAPON"));
        Anim.SetBool("wpnBFG", GUI.Toggle(new Rect(0, 210, 150, 25), Anim.GetBool("wpnBFG"), "BIG FUCKING GUN"));
        GUI.EndGroup();

     
    }
}
