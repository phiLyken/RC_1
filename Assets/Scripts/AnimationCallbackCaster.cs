using UnityEngine;
using System.Collections;


public delegate void StringEventHandler(string str);

public class AnimationCallbackCaster : MonoBehaviour {

    public EventHandler OnWeaponHide;
    public EventHandler OnWeaponShow;

    public StringEventHandler OnAbilityTrigger;


    public void WeaponShow()
    {
        if (OnWeaponShow != null)
            OnWeaponShow();
    }

    public void WeaponHide()
    {
        if (OnWeaponHide != null)
        {
            OnWeaponHide();
        }
    }

    public void AbilityCallback(string id)
    {
      //  Debug.Log("Callback " + id);
        if (OnAbilityTrigger != null)
        {
            OnAbilityTrigger(id);
        }
      
    }
}
