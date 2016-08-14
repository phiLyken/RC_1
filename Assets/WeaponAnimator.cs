using UnityEngine;
using System.Collections;


[System.Serializable]
public class WeaponAnimator
{
    Animator animator;

    public WeaponAnimator(GameObject weapon_part)
    {
        animator = weapon_part.GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogWarning("No Animator on Weaponpart " + weapon_part.name);
        }
    }

    public void ShowWeapon()
    {
        if (animator == null)
            return;

        animator.gameObject.SetActive(true);
    }
    public void HideWeaon()
    {
        if (animator == null)
            return;


        animator.gameObject.SetActive(false);
    }

    public void PlayShoot()
    {
        if (animator == null)
            return;

        animator.SetTrigger("bShooting");
        //TODO EFFECT PLAYING GOES HERE
    }
}
