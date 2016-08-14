using UnityEngine;
using System.Collections;


public class UnitAnimationController : MonoBehaviour
{
    public UnitAnimation UnitAnimator;
    public Unit m_Unit;

    public void Init(Unit u)
    {
        m_Unit = u;
        UnitAnimator = new UnitAnimation();

        GameObject left_weapon = u.Inventory.EquipedWeapon.WeaponMesh.AttachmentLeft;
        GameObject right_weapon = u.Inventory.EquipedWeapon.WeaponMesh.AttachmentRight;

        int index = u.Inventory.EquipedWeapon.WeaponMesh.WeaponIndex;
        UnitAnimator.Init(
            m_Unit.GetComponent<Animator>(),
            new WeaponAnimator(right_weapon),
            new WeaponAnimator(left_weapon), index
        );
      

        m_Unit.Actions.OnActionStarted += action =>
        {
            if (action.GetType() == typeof(UnitAction_Move))
            {
                UnitAnimator.SetWalking(true);

            }
            else
            {
                UnitAnimator.SetWalking(true);
            }
        };

        m_Unit.Actions.OnActionComplete += action =>
        {
            if (action.GetType() == typeof(UnitAction_Move))
            {
                UnitAnimator.SetWalking(false);
            }
        };

        UnitAnimator.SetWeaponIndex((int) m_Unit.GetComponent<UnitInventory>().EquipedWeapon.AnimationState);
    }

    public void WeaponShow()
    {
        UnitAnimator.WeaponShow();
    }

    public void WeaponHide()
    {
        UnitAnimator.WeaponHide();
    }
}

