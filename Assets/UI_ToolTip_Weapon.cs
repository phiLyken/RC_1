using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class UI_ToolTip_Weapon : UI_ToolTip_Base {

    public Text Title;
    public Text Range;
    public Text Normal_Dmg;
    public Text Int_Dmg;

    public Text IntBonus;
    public Text IntAttack;

    public void SetWeapon(Weapon weapon)
    {

        Title.text = weapon.GetID();
      //  Range.text = weapon..ToString();

     //   IntBonus.text = weapon.RegularBehavior.IntBonus.IntBonusText();

      //  IntAttack.text = weapon.IntAttackBehavior.Get

    }

    public override void SetItem(object obj)
    {
        SetWeapon((Weapon)obj);
    }
}
