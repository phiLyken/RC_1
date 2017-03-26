using UnityEngine;
using System.Collections;

public class Unit_EasterEgg : MonoBehaviour {

    UnitAnimation m_animation;
    bool hovered;
    int count;

    public void Init(UnitAnimation anim)
    {
        m_animation = anim;        
    }	
  

    void OnMouseEnter()
    {
        hovered = true;
    }

    void OnMouseExit()
    {
        hovered = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && hovered)
        {
            Count();
        }
    }

    void Count()
    {  

        count++;
        if (count == 10)
        {
            TrackingManager.TrackingCall_EasterEgg("10", PlayerLevel.Instance.GetCurrentLevel());
            WaypointMover mover = gameObject.AddComponent<WaypointMover>();
            UnitRotationController contr = gameObject.AddComponent<UnitRotationController>().Init(mover, _foo);
            contr.TurnToPosition(Camera.main.transform, () => m_animation.SetTrigger(UnitAnimationTypes.bAggro.ToString()));
        }
        else if (count == 20)
        {
            TrackingManager.TrackingCall_EasterEgg("20", PlayerLevel.Instance.GetCurrentLevel());
            m_animation.SetTrigger(UnitAnimationTypes.bDying.ToString());
     

        }
        else if ( count == 3 || ( count > 3 && M_Math.Roll(0.1f)))
        {
            TrackingManager.TrackingCall_EasterEgg("3", PlayerLevel.Instance.GetCurrentLevel());

            m_animation.SetTrigger(UnitAnimationTypes.bHit.ToString());

        } 
    }

     Vector3 _foo()
    {
        return Vector3.zero;
    }

   
}
