using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public delegate void EffectEventHandler(UnitEffect effect);
[System.Serializable]
public class UnitEffect : MonoBehaviour
{

    public EffectSpawner VFX_Tick;
    public EffectSpawner VFX_Applied;

    public enum TargetModes
    {   Owner, Target  }

    protected bool isCopy = false;


    public object Instigator;
    public string Unique_ID;
    public TargetModes TargetMode;
    public Sprite Icon;  

    public bool FocusOnCaster;
    public bool FocusOnTarget;
    public bool ShowRemoveNotification;
    public bool ShowApplyNotification;
    public bool TickOnApply;
    public int TickFrequency;
    public int MaxDuration;
    public bool ReplaceEffect;

    [HideInInspector]
    public float EffectBonus = 1;

    protected Unit Effect_Host;

    protected int _durationActive;

    public EffectEventHandler OnEffectExpired;
    public EffectEventHandler OnEffectTick;

    public Unit GetTarget(Unit target, Unit instigator)
    {
        return (TargetMode == TargetModes.Owner) ? instigator : target;
    }

    public void Init(UnitAction_ApplyEffect owner)
    {
        
        EffectBonus = 1;
        Instigator = owner;


 
    }


    public virtual UnitEffect MakeCopy(UnitEffect origin, Unit host)
    {

        UnitEffect _cc = origin.gameObject.Instantiate(host.transform, true).GetComponent<UnitEffect>();


        _cc.name = Unique_ID + "_copy";
        _cc.Effect_Host = host;
        _cc.isCopy = true;

        return _cc;
    }


    public void OnDestroy()
    {
    //    Debug.Log("Removed Effect  -" + Unique_ID);
        TurnSystem.Instance.OnGlobalTurn -= OnGlobalTurn;
    }

    public IEnumerator ApplyEffectSequence(Unit target, UnitAction_ApplyEffect instigator) {

        Instigator = instigator;
        target = GetTarget(target, instigator.GetOwner() );
        
        if (FocusOnCaster && RC_Camera.Instance != null) {
            Debug.Log("^effects go to to " + instigator.GetOwner());
            RC_Camera.Instance.ActionPanToPos.GoTo(instigator.GetOwner().currentTile.GetPosition());
            yield return new WaitForSeconds(0.5f);
        }

         

        if (FocusOnTarget && RC_Camera.Instance != null) {
            Debug.Log("^effects to to " + target.GetID());
            RC_Camera.Instance.ActionPanToPos.GoTo(target.currentTile.GetPosition());
            yield return new WaitForSeconds(0.5f);
        }       
        
        AttemptApplyEffect(target, this);

    }



    protected virtual bool CanApplyEffect(Unit target, UnitEffect effect)
    {
        return !target.IsDead();
    }

    private void AttemptApplyEffect(Unit target, UnitEffect effect)
    {
        //Make copy
        UnitEffect copy = MakeCopy(effect, target);
        Debug.Log("^effectsAttempt Apply \n" + effect.ToString());
        if (CanApplyEffect(target, copy) && target.GetComponent<Unit_EffectManager>().ApplyEffect(copy))
        {
            
                TurnSystem.Instance.OnGlobalTurn += copy.OnGlobalTurn;

                if (VFX_Applied != null)
                    VFX_Applied.Init(target.gameObject);
               

                if (TickOnApply)
                    copy.EffectTick();
         } else
        {
            Destroy(copy.gameObject);
        }

     
    }
    #region

    ///OVERIDE THIS
    /// 
    protected virtual void EffectTick()
    {

    }
    protected virtual void GlobalTurnTick()
    {
    }
 

    /// <summary>
    /// Call After Ticking
    /// </summary>
    protected void Ticked()
    {
        if (OnEffectTick != null) OnEffectTick(this);
        Debug.Log(GetToolTipText() + " TICKED");



    }

    public void Remove()
    {
        if (OnEffectExpired != null)
            OnEffectExpired(this);

        Debug.Log("^effects Global remove " + Effect_Host.GetID() + "  -" + Unique_ID);

        TurnSystem.Instance.OnGlobalTurn -= OnGlobalTurn;
 

 
    }
    public virtual void SetPreview(UI_DmgPreview prev, Unit target) { }
    #endregion

    protected  void OnGlobalTurn(int t)
    {
        _durationActive++;


        if (_durationActive > MaxDuration)
        {
            Remove();
            return;
        }

        if (!Effect_Host.IsDead() && (_durationActive % Mathf.Max(1,TickFrequency)) == 0)
        { 
            GlobalTurnTick();
            if (VFX_Tick != null)
                VFX_Tick.Init(Effect_Host.gameObject);
        }
    }

    public virtual string GetToolTipText()
    {
        return " null";
    }

    public virtual string GetShortHandle()
    {
        return  " no name";
    }
    public virtual string GetNotificationText()
    {
        return " foo";
    }
    public int   GetMaxDuration()
    {
        return MaxDuration;
    }

    public  int GetDurationLeft()
    {
        return (MaxDuration - _durationActive);
    }

    public void UpdateBonus()
    {
        EffectBonus = Constants.GetAdrenalineBonus((Instigator as UnitAction_ApplyEffect).GetOwner().Stats);
    }

    public override string ToString()
    {
        return Unique_ID + " \nTargetmode" + TargetMode + "  " + " MaxDuration:" + MaxDuration + "  TickOnApply:" + TickOnApply+"\n";

        /*

        public object Instigator;
    public string Unique_ID;
    public TargetModes TargetMode;
    public Sprite Icon;  

    public bool FocusOnCaster;
    public bool FocusOnTarget;
    public bool ShowRemoveNotification;
    public bool ShowApplyNotification;
    public bool TickOnApply;
    public int TickFrequency;
    public int MaxDuration;
    public bool ReplaceEffect;*/
    }
}
 