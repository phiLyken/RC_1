using UnityEngine;
using System.Collections;

public class Unit_EasterEgg : MonoBehaviour {

    UnitAnimation m_animation;
    bool hovered;
    
    bool ActionInProgress;
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
            WaypointMover mover = gameObject.AddComponent<WaypointMover>();
            UnitRotationController contr = gameObject.AddComponent<UnitRotationController>().Init(mover, _foo);
            contr.TurnToPosition(Camera.main.transform, () => m_animation.SetTrigger(UnitAnimationTypes.bAggro.ToString()));
        }
        else if (count == 20)
        {
            m_animation.SetTrigger(UnitAnimationTypes.bDying.ToString());
            StartCoroutine(WTF());

        }
        else if ( count == 3 || ( count > 3 && M_Math.Roll(0.1f)))
        {
            m_animation.SetTrigger(UnitAnimationTypes.bHit.ToString());

        } 
    }

     Vector3 _foo()
    {
        return Vector3.zero;
    }

    IEnumerator WTF()
    {
        yield return new WaitForSeconds(0.5f);
        //ToastNotification.SetToastMessage1("OFFICER?");
        yield return new WaitForSeconds(1f);
       // ToastNotification.SetToastMessage1("OFFICER?!?!");
        yield return new WaitForSeconds(1f);
      //  ToastNotification.SetToastMessage1("OFFICEEEEEEEERRR!!!!");
    }
}
