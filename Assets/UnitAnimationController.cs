using UnityEngine;
using System.Collections;


public class UnitAnimationController : MonoBehaviour
{
  

    public UnitAnimation UnitAnimator;
    public Unit m_Unit;

    public void Init(Unit u, GameObject mesh)
    {
        m_Unit = u;
 
        int index = u.Inventory.EquipedWeapon.WeaponMesh.WeaponIndex;
        UnitAnimator = UnitFactory.MakeUnitAnimations(mesh, u.Inventory.EquipedWeapon.WeaponMesh, index, mesh.AddComponent<AnimationCallbackCaster>());
        
        m_Unit.Actions.OnTargetAction += (a, b) => { UnitAnimator.SetAimTarget(b); };

        m_Unit.Actions.OnActionStarted += action =>
        {
            if (action.GetType() == typeof(UnitAction_Move))
            {
                OnMoveStart(action as UnitAction_Move);
            }
        };

        m_Unit.Stats.OnDmgReceived += () =>
        {
 
            PlayAnimation(UnitAnimationTypes.bHit);
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

    public void PlayAnimation(UnitAnimationTypes anim)
    {
      
        UnitAnimator.SetTrigger(anim.ToString());
    }
   
    public void WaitForExection(UnitAnimationTypes anim, EventHandler on_exec)
    {        
        PlayAnimation(anim);
        if (UnitAnimator.OnExec != null)
            UnitAnimator.OnExec();
        UnitAnimator.OnExec = on_exec;
 
        
    }
}

