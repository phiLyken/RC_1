using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectListTest : MonoBehaviour {

    public List<UnitEffect> Effects;
    public List<UnitEffect> Effects2;

    public UI_EffectListView View;
    public UI_EffectListView View2;

    void Start()
    {
        View.SetEffects(Effects);
        View2.SetEffects(Effects2);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            View.SetEffects(Effects2);
            View2.SetEffects(Effects);
        }

        if (Input.GetButtonDown("Fire2"))
        {  

            View.SetEffects(Effects);
             View2.SetEffects(Effects2);
         }

    }

}
