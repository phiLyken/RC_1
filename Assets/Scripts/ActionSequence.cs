using UnityEngine;
using System.Collections;

public class ActionSequence : MonoBehaviour {


    Unit m_Unit;
    

    public void Init( Unit unit)
    {

        m_Unit = unit;
    }
    public void StartSequence(UnitAnimationTypes anim, Unit caster, Transform target, EventHandler callback_execute)
    {
        StartCoroutine(Sequence(anim, caster,  target, callback_execute));
    }

    IEnumerator Sequence(UnitAnimationTypes anim, Unit caster,  Transform target, EventHandler callback_execute)
    {
        UnitAnimationController anim_controller = m_Unit.GetComponentInChildren<UnitAnimationController>();
        UnitRotationController rotation_controller = m_Unit.GetComponentInChildren<UnitRotationController>();
        
        TurnEventQueue.CameraFocusEvent cam_pan = new TurnEventQueue.CameraFocusEvent(caster.transform.position);

        yield return StartCoroutine(cam_pan.WaitForEvent());
       

        if(rotation_controller != null)
            yield return StartCoroutine(rotation_controller.TurnToTargetPositiom(target, null));

        if(anim_controller != null)
            anim_controller.PlayAnimation(anim, callback_execute);

        OnExected(caster, target);
    }      

    protected virtual void OnExected(Unit caster, Transform target)
    {

    }
}
