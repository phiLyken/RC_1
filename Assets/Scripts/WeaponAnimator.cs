using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;


[System.Serializable]
public class WeaponAnimator
{
    Animator animator;
    GameObject part;
    ShootBullet fx;

    public bool ShootEffectsPlaying;

    public Action<Transform> OnShoot;


    public WeaponAnimator(GameObject weapon_part, ShootBullet fx_prefab, GameObject muzzle_target)
    {
		if(weapon_part != null){
	        part = weapon_part;

            if(fx_prefab != null)
            {
                fx = GameObject.Instantiate(fx_prefab).GetComponent<ShootBullet>();
                fx.transform.SetParent(muzzle_target == null ? part.transform : muzzle_target.transform);
                fx.transform.localPosition = Vector3.zero;
            }

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

    public void PlayShoot(Transform Target, EventHandler callback)
    {
        if (animator != null)
            animator.SetTrigger("bShooting");

        if(Target != null)
        {
            //Debug.Log("Play Shoot " +part + " "+Target.name);
        

            if(fx == null){
                callback();
                //Debug.LogWarning(part + " has no shoot effects");
            } else {
                ShootEffectsPlaying = true;
                Sequence shooting =    fx.Shoot(Target).Play();
                shooting.AppendCallback(() => {
                 //   Debug.Log("shooting done playing");
                    ShootEffectsPlaying = false;
                    if (callback != null)
                        callback();
                });
            }
        }
    }

 
}
