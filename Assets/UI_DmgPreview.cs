﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_DmgPreview : MonoBehaviour {

    public Text MainTF;
    public Text IconTF;

    public Image Icon;

    public static UI_DmgPreview Instance;

    public void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        UnitAction_Attack.OnTarget += SetDamage;
        Disable();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void SetDamage(Unit instigator, Unit target, IUnitEffect effect)
    {
      //  Debug.Log("assad");
        gameObject.SetActive(true);

        effect.SetPreview(this, target);

        GetComponent<UI_WorldPos>().SetWorldPosObject(target.transform); 
    }

    void Update()
    {
        GetComponent<UI_WorldPos>().UpdatePos();
    }

}
