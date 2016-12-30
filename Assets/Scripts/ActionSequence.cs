using UnityEngine;
using System.Collections;

public class ActionSequence : MonoBehaviour {

  

    UnitAnimationController animation_controller;
    UnitRotationController rotation_controller;

    public void Init( Unit unit)
    {
        animation_controller = unit.GetComponentInChildren<UnitAnimationController>() ;
        rotation_controller = unit.GetComponentInChildren<UnitRotationController>();
    }
    public void StartSequence(UnitAnimationTypes anim, Unit caster, Transform target, EventHandler callback_execute)
    {
        StartCoroutine(Sequence(anim, caster,  target, callback_execute));
    }

    IEnumerator Sequence(UnitAnimationTypes anim, Unit caster,  Transform target, EventHandler callback_execute)
    {
        yield return StartCoroutine(rotation_controller.TurnToTargetPositiom(target, null));
        animation_controller.PlayAnimation(anim, callback_execute);
        OnExected(caster, target);
    }      

    protected virtual void OnExected(Unit caster, Transform target)
    {

    }
}
