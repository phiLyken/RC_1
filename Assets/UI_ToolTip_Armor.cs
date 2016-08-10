using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Linq;

public class UI_ToolTip_Armor : UI_ToolTip_Base {

    public Text TF_Max;
    public Text TF_MoveRange;
    public Text TF_Time;

    public void SetArmor(Armor conf)
    {
        /*
        foreach(Stat inf in conf.BuffedStats)
        {
            if (inf.StatType == UnitStats.StatType.max) TF_Max.text = inf.Amount.ToString() ;
            if (inf.StatType == UnitStats.StatType.move_range) TF_MoveRange.text = inf.Amount.ToString();
            if (inf.StatType == UnitStats.StatType.speed) TF_Time.text = inf.Amount.ToString();
        }
       
      */

      //  TF_MoveRange.text = (conf.BuffedStats.Where(b => b.Stat == UnitStats.StatType.move_range) as StatInfo).Amount.ToString();
       // TF_Time.text = (conf.BuffedStats.Where(b => b.Stat == UnitStats.StatType.speed) as StatInfo).Amount.ToString();
    }

    public override void SetItem(object obj)
    {
        SetArmor((Armor)obj);
    }
}
