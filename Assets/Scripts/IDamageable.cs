using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void DamageEventHandler(Effect_Damage dmg);

[System.Serializable]
public class UnitEffect
{
    public Sprite Icon;
       
    public int TurnLength;

    public virtual void ApplyToUnit(Unit u) { }
    public virtual void SetPreview(UI_DmgPreview prev, Unit target) { }

    public UnitEffect(UnitEffect origin)
    {
        Icon = origin.Icon;
        TurnLength = origin.TurnLength;
    }

    public UnitEffect() { }

    public virtual string GetString()
    {
        return " null";
    }

    public virtual void SpawnEffect(Transform instigator, Unit target)
    {
       
    }
}

[System.Serializable]
public class Effect_Damage : UnitEffect
{
    public MyMath.R_Range DamageRange;

    int baked_damage = -1;

    public Effect_Damage(Effect_Damage origin) : base(origin)
    {
        DamageRange = origin.DamageRange;

        
        baked_damage = (int) origin.DamageRange.Value();

        Debug.Log("DMG  Baked "+ baked_damage);
    }

    public int GetDamage()
    {
        if(baked_damage < 0)
        {
            Debug.LogWarning("DMG NOT BAKED");
        } 
        return baked_damage;
    }
    public Effect_Damage()
    {
        baked_damage = 5;
    }
    public override void SpawnEffect(Transform instigator, Unit target)
    {
        if (PanCamera.Instance != null)
            PanCamera.Instance.PanToPos(target.currentTile.GetPosition());

        Debug.Log("EFFEEFCADASD");
        SetLazer.MakeLazer(0.5f, new List<Vector3> { instigator.transform.position, target.transform.position }, Color.red);

    }
    public override string GetString()
    {
        return GetDamage() + " DAMAGE";
    }
    /// <summary>
    /// clones itself to the target
    /// </summary>
    /// <param name="target"></param>
    public override void ApplyToUnit(Unit target)
    {
        Effect_Damage eff = new Effect_Damage(this);
     
        if(eff.GetDamage() > 0 && !target.IsDead() ) { 
            EffectNotification.SpawnDamageNotification(target.transform, eff);          
            
            target.ReceiveDamage(eff);
         
        }
    }

    public override void SetPreview(UI_DmgPreview prev, Unit target)
    {
       
    }

}

[System.Serializable]
public class Heal : UnitEffect
{
    public string foo;

    public override void ApplyToUnit(Unit target)
    {
        (target.Stats as PlayerUnitStats).Rest();   
    }

    public Heal(Heal origin) : base(origin)
    {
        
    }

    public Heal()
    {

    }
    public override void SetPreview(UI_DmgPreview prev, Unit target)
    {
        prev.Icon.gameObject.SetActive(false);
        prev.MainTF.text = "RESTORE O²";

        prev.IconTF.text = ((target.Stats as PlayerUnitStats).Max - (target.Stats as PlayerUnitStats).Will).ToString(); 
    }

} 

public interface IDamageable {

    void ReceiveDamage(Effect_Damage dmg);  

}


