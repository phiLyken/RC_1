using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class UnitAnimation
{
    Animator unit_animator;



    
    GetInt GetIdle;
    GetBool GetRage;
    WeaponAnimator WeaponAnimator_Right;
    WeaponAnimator WeaponAnimator_Left;

    Transform AimTarget;

    EventHandler OnExec;
    bool WaitForExecution;

    public void SetExec(EventHandler new_exec)
    {
        if(OnExec != null)
        {
            OnExec();
        }
        OnExec = new_exec;
    }
    public UnitAnimation Init(Animator unit, WeaponAnimator right, WeaponAnimator left, float index, AnimationCallbackCaster callback, GetInt get_id, GetBool rs  )
    {
        GetIdle = get_id;
        GetRage = rs;
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
       MDebug.Log("set weapon index " + f);
        unit_animator.SetFloat("WeaponIndex", (int) f);
    }

    public void SetAimTarget(Transform tr)
    {
       // MDebug.Log("set aim target  "+tr.gameObject.name);
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

    public void SetIdleIndex(float b)
    {
        unit_animator.SetFloat("IdleIndex", (int) b);
    }

    void AttemptExection()
    {
    
        if( OnExec != null && WaitForExecution && !WeaponAnimator_Left.ShootEffectsPlaying && !WeaponAnimator_Right.ShootEffectsPlaying)
        {
            WaitForExecution = false;
            OnExec();
            OnExec = null;
        }

      //  MDebug.Log("attempted to callback");
    }
    public void AbilityCallback(string id)
    {   
        switch (id)
        {
            case "idle_end":
                unit_animator.SetFloat("IdleIndex", GetIdle(GetRage()  ));
                break;
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
        MDebug.Log("^animationCALL BACK Weapon Hide");
        WeaponAnimator_Left.HideWeapon();
        WeaponAnimator_Right.HideWeapon();
    }

    public void WeaponShow()
    {
        MDebug.Log("^animationCALL BACK* Weapon Show");
        WeaponAnimator_Left.ShowWeapon();
        WeaponAnimator_Right.ShowWeapon();
    }

    public void SetTrigger(string id)
    {
      //  MDebug.Log(id);
        unit_animator.SetTrigger(id);
    }
}
