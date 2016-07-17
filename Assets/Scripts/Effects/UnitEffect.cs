using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public delegate void EffectEventHandler(UnitEffect effect);
[System.Serializable]
public class UnitEffect 
{
    public enum TargetModes
    {   Owner, Target  }

    public string Unique_ID;
    public TargetModes TargetMode;
    public Sprite Icon;
    public GameObject ApplyEffectOnTargetPrefab;
    public GameObject CastEffectOnInstigatorPrefab;
    public bool MakeLazer;
    public Color LazerColor;

    public bool FocusOnCaster;
    public bool FocusOnTarget;
    public bool ShowRemoveNotification;
    public bool ShowApplyNotification;
    public bool TickOnApply;

    public float castDelay;

    public int MaxDuration;
    public bool ReplaceEffect;

    protected Unit Effect_Host;

    protected int _durationActive;

    public EffectEventHandler OnEffectExpired;
    public EffectEventHandler OnEffectTick;

    public Unit GetTarget(Unit target, Unit instigator)
    {
        return (TargetMode == TargetModes.Owner) ? instigator : target;
    }

    public IEnumerator ApplyEffectSequence(Unit target, Unit instigator) {

        target = GetTarget(target, instigator);

        Debug.Log(target.name);

        if (FocusOnCaster && PanCamera.Instance != null) {
            PanCamera.Instance.PanToPos(instigator.currentTile.GetPosition());
            yield return new WaitForSeconds(0.5f);
        }

        if (CastEffectOnInstigatorPrefab != null) {
            GameObject.Instantiate(CastEffectOnInstigatorPrefab, instigator.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(castDelay);
        }

        if (MakeLazer)
        {
            SetLazer.MakeLazer(0.25f, new List<Vector3> { instigator.transform.position, target.transform.position }, LazerColor);
        }

        if (FocusOnTarget && PanCamera.Instance != null) {
            PanCamera.Instance.PanToPos(target.currentTile.GetPosition());
            yield return new WaitForSeconds(0.5f);
        }

        if (ApplyEffectOnTargetPrefab != null)
        {
            GameObject.Instantiate(ApplyEffectOnTargetPrefab, target.transform.position, Quaternion.identity);
        }


        AttemptApplyEffect(target, this);

    }


    public virtual UnitEffect MakeCopy(UnitEffect origin)
    {
        
        return origin.MemberwiseClone() as UnitEffect;
    }

   

    protected virtual bool CanApplyEffect(Unit target, UnitEffect effect)
    {
        return !target.IsDead();
    }

    private void AttemptApplyEffect(Unit target, UnitEffect effect)
    {
        //Make copy
        UnitEffect copy = MakeCopy(effect);
        copy.Effect_Host = target;
   

        if (CanApplyEffect(target, copy))
        {
            if (target.GetComponent<Unit_EffectManager>().ApplyEffect(copy))
            {
                TurnSystem.Instance.OnGlobalTurn += copy.OnGlobalTurn;

                if (TickOnApply)
                    copy.EffectTick();
            }

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

    protected virtual void EffectRemoved()
    {
    }




    /// <summary>
    /// Call After Ticking
    /// </summary>
    protected void Ticked()
    {
        if (OnEffectTick != null) OnEffectTick(this);
        Debug.Log(GetString() + " TICKED");
        _durationActive++;

        if (_durationActive > MaxDuration)
        {
            if (OnEffectExpired != null) OnEffectExpired(this);
            TurnSystem.Instance.OnGlobalTurn -= OnGlobalTurn;
            EffectRemoved();
        }
    }




public UnitEffect() { }

    public virtual void SetPreview(UI_DmgPreview prev, Unit target) { }
    #endregion

    protected  void OnGlobalTurn(int t)
    {
        if (!Effect_Host.IsDead())
            GlobalTurnTick();
    }
   
      

    public virtual string GetString()
    {
        return " null";
    }


}