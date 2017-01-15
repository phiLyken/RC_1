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

        public DeathEvent(Unit u)
        {
            EventID = "UNIT DEATH";
            StartEvent();
            m_unit = u;

            UnitAnimationController animationController = m_unit.GetComponentInChildren<UnitAnimationController>();
            if (animationController != null)
            {
                animationController.PlayAnimation(UnitAnimationTypes.bDying, null);
            }
            
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
