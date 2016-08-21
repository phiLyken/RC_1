using UnityEngine;

public class UnitAnimation
{
    Animator unit_animator;

    WeaponAnimator WeaponAnimator_Right;
    WeaponAnimator WeaponAnimator_Left;

    public UnitAnimation Init(Animator unit, WeaponAnimator right, WeaponAnimator left, float index, AnimationCallbackCaster callback)
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
        Debug.Log("set weapon index " + f);
        unit_animator.SetFloat("WeaponIndex", (int) f);
    }

    public void SetWalking(bool b)
    {
        unit_animator.SetBool("bMoving", b);
    }

    public void AbilityCallback(string id)
    {
        Debug.Log("Ability call back " + id);
        switch (id)
        {
            case "shoot_left":
                WeaponAnimator_Left.PlayShoot();
                break;

            case "shoot_right":
                WeaponAnimator_Right.PlayShoot();
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
        unit_animator.SetTrigger(id);
    }
}
