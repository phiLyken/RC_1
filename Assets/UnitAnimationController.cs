using UnityEngine;
using System.Collections;


public class UnitAnimationController : MonoBehaviour {
    
    Animator anim;

    public void Init(Unit unit)
    {
        anim = unit.GetComponent<Animator>();

        unit.Actions.OnActionStarted += action =>
        {
            if( action.GetType() == typeof(UnitAction_Move))
            {
                anim.SetBool("moving", true);
                
            } else { 
                anim.SetTrigger(action.Animation.ToString());
            }
        };

        unit.Actions.OnActionComplete += action =>
        {
            if (action.GetType() == typeof(UnitAction_Move))
            {
                anim.SetBool("moving", false);
            }
        };

        anim.SetFloat("WeaponIndex",(int) unit.GetComponent<UnitInventory>().EquipedWeapon.AnimationState);
        
    }
}
