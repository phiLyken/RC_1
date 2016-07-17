using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class UI_ToolTip_Weapon : MonoBehaviour {

    public Text Title;
    public Text Range;
    public Text Normal_Dmg;
    public Text Int_Dmg;

    public Text IntBonus;
    public Text IntAttack;

    public void SetWeapon(WeaponConfig weapon)
    {

        Title.text = weapon.GetID();
        Range.text = weapon.Range.ToString();


    }
}
