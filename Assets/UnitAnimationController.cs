using UnityEngine;
using System.Collections;


public class UnitAnimationController : MonoBehaviour
{
    public UnitAnimation UnitAnimator;
    public Unit m_Unit;

    public void Init(Unit u, GameObject mesh)
    {
        m_Unit = u;
        UnitAnimator = new UnitAnimation();

        GameObject left_weapon = u.Inventory.EquipedWeapon.WeaponMesh.AttachmentLeft;
        GameObject right_weapon = u.Inventory.EquipedWeapon.WeaponMesh.AttachmentRight;

        int index = u.Inventory.EquipedWeapon.WeaponMesh.WeaponIndex;

     //   Debug.Log(index);
        UnitAnimator.Init(
            mesh.GetComponent<Animator>(),
            new WeaponAnimator(right_weapon),
            new WeaponAnimator(left_weapon), index,
            mesh.AddComponent<AnimationCallbackCaster>()
            );

      //  m_Unit.Actions.GetActionOfType<UnitAction_Move>().OnActionComplete += OnMoveEnd;
     //   m_Unit.Actions.GetActionOfType<UnitAction_Move>().OnExecuteAction += OnMoveStart;


        m_Unit.Actions.OnActionStarted += action =>
        {
            if (action.GetType() == typeof(UnitAction_Move))
            {
                OnMoveStart(action as UnitAction_Move);
            }
        };



        
    }

    void OnMoveStart(UnitAction_Move m)
    {
        UnitAnimator.SetWalking(true);
        m.OnActionComplete += OnMoveEnd;

    }

    void OnMoveEnd(UnitActionBase m)
    {
        UnitAnimator.SetWalking(false);
        m.OnActionComplete -= OnMoveEnd;
    }

   
}

