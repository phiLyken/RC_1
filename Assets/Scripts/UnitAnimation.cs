using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class UnitAnimation
{
    Animator unit_animator;

    WeaponAnimator WeaponAnimator_Right;
    WeaponAnimator WeaponAnimator_Left;

    Transform AimTarget;

    public  EventHandler OnExec;
    bool WaitForExecution;

    public UnitAnimation Init(Animator unit, WeaponAnimator right, WeaponAnimator left, float index, AnimationCallbackCaster callback )
    {
        WeaponAnimator_Left = left;
        WeaponAnimator_Right = right;
        unit_animator = unit;

       
        SetWeaponIndex(index);

 

            //No multicast so we dont need to remove listeners
            callback.OnAbilityTrigger = AbilityCallback;
            callback.OnWeaponHide = WeaponHide;
            callback.OnWeaponShow = WeaponShow;
       



        return this;
    }

    public void SetWeaponIndex(float f)
    {
      //  Debug.Log("set weapon index " + f);
        unit_animator.SetFloat("WeaponIndex", (int) f);
    }

    public void SetAimTarget(Transform tr)
    {
        Debug.Log("set aim target  "+tr.gameObject.name);
        AimTarget = tr.FindDeepChild("humanoid");

    }
    void OnShotEnd()
    {
        AttemptExection();
    }

    public void SetWalking(bool b)
    {
        unit_animator.SetBool("bMoving", b);
    }

    void AttemptExection()
    {
    
        if( OnExec != null && WaitForExecution && !WeaponAnimator_Left.ShootEffectsPlaying && !WeaponAnimator_Right.ShootEffectsPlaying)
        {
            WaitForExecution = false;
            OnExec();
            OnExec = null;
        }

        Debug.Log("attempted to callback");
    }
    public void AbilityCallback(string id)
    {
   
        switch (id)
        {
            case "shoot_left":
                WeaponAnimator_Left.PlayShoot(AimTarget, OnShotEnd);
                break;

            case "shoot_right":
                WeaponAnimator_Right.PlayShoot(AimTarget, OnShotEnd);
                break;
            case "ability_exec":
                WaitForExecution = true;
                AttemptExection();
                break;
        }
    }
    
 
    public void WeaponHide()
    {
        Debug.Log(" *ANIMATION CALL BACK* Weapon Hide");
        WeaponAnimator_Left.HideWeapon();
        WeaponAnimator_Right.HideWeapon();
    }

    public void WeaponShow()
    {
        Debug.Log(" *ANIMATION CALL BACK* Weapon Show");
        WeaponAnimator_Left.ShowWeapon();
        WeaponAnimator_Right.ShowWeapon();
    }

    public void SetTrigger(string id)
    {
        Debug.Log(id);
        unit_animator.SetTrigger(id);
    }
}
