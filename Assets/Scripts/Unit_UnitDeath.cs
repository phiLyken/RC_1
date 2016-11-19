using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Unit_UnitDeath : MonoBehaviour {

    Unit m_unit;

    void Awake()
    {
        m_unit = gameObject.GetComponent<Unit>();

        Unit.OnUnitKilled += KilledUnitSequence;
    }

    void KilledUnitSequence(Unit u)
    {
        if(u == m_unit)
        {
            Unit.OnUnitKilled -= KilledUnitSequence;
           //  Debug.Log(" death " + u.GetID());
             new DeathEvent(u);
             new TurnEventQueue.CameraFocusEvent(u.currentTile.GetPosition());
        }

    }

   
    class DeathEvent : TurnEventQueue.TurnEvent
    {

        Unit m_unit;

        //TOCO FIXME DEATH CALLBACK

 
        public DeathEvent(Unit u)
        {
            StartEvent();
            m_unit = u;
            m_unit.GetComponentInChildren<UnitAnimationController>().WaitForExection(UnitAnimationTypes.bDying, null);
            
            Sequence seq = DOTween.Sequence().InsertCallback(4f, () => EndEvent() );
            seq.Play();


        }

        public override void EndEvent()
        {

            Destroy(m_unit.gameObject);
            base.EndEvent();
        }
    }
}
