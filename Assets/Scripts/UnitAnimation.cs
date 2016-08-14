using UnityEngine;

public class UnitAnimation
{
    Animator unit_animator;

    WeaponAnimator WeaponAnimator_Right;
    WeaponAnimator WeaponAnimator_Left;

    public UnitAnimation Init(Animator unit, WeaponAnimator right, WeaponAnimator left, float index)
    {
        WeaponAnimator_Left = left;
        WeaponAnimator_Right = right;
        unit_animator = unit;

        SetWeaponIndex(index);
        return this;
    }

    public void SetWeaponIndex(float f)
    {
        unit_animator.SetFloat("WeaponIndex", (int) f);
    }

    public void SetWalking(bool b)
    {
        unit_animator.SetBool("bMoving", b);
    }

    public void AbilityCallback(string id)
    {
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
        WeaponAnimator_Left.HideWeaon();
        WeaponAnimator_Right.HideWeaon();
    }

    public void WeaponShow()
    {
        WeaponAnimator_Left.ShowWeapon();
        WeaponAnimator_Right.ShowWeapon();
    }

    public void SetTrigger(string id)
    {
        unit_animator.SetTrigger(id);
    }
}
