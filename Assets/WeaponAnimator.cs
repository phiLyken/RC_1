using UnityEngine;
using System.Collections;


[System.Serializable]
public class WeaponAnimator
{
    Animator animator;
    GameObject part;
    public WeaponAnimator(GameObject weapon_part)
    {
		if(weapon_part != null){
	        part = weapon_part;
	        animator = weapon_part.GetComponent<Animator>();
	        if(animator == null)
	        {
	            Debug.LogWarning("No Animator on Weaponpart " + weapon_part.name);
	        }
		} else
        {
           // Debug.Log("no weapon part");
        }
    }

    public void ShowWeapon()
    {
        if (part == null)
            return;


        part.SetActive(true);
    }
    public void HideWeapon()
    {
        if (part == null)
            return;


        part.SetActive(false);
    }

    public void PlayShoot(Transform Target)
    {
        if (animator == null)
            return;
        Debug.Log("Play Shoot " + animator.gameObject.name);
        animator.SetTrigger("bShooting");
        //TODO EFFECT PLAYING GOES HERE
    }
}
