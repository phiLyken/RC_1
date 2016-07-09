using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void EffectEventHandler(UnitEffect effect);
[System.Serializable]
public class UnitEffect
{
    public enum TargetModes
    {
        Owner, Target
    }

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

    public float castDelay;

    public int MaxDuration;
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

        if(CastEffectOnInstigatorPrefab != null) {
            GameObject.Instantiate(CastEffectOnInstigatorPrefab, instigator.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(castDelay);
        }

        if (MakeLazer)
        {
            SetLazer.MakeLazer(0.25f, new List<Vector3> { instigator.transform.position, target.transform.position }, LazerColor);
        }

        if (FocusOnTarget && PanCamera.Instance != null){ 
            PanCamera.Instance.PanToPos(target.currentTile.GetPosition());
            yield return new WaitForSeconds(0.5f);
        }

        if(ApplyEffectOnTargetPrefab != null)
        {
            GameObject.Instantiate(ApplyEffectOnTargetPrefab, target.transform.position, Quaternion.identity);
        }

        yield return ApplyEffect(target, this);

    }

    #region
    ///OVERIDE THIS
    protected virtual IEnumerator ApplyEffect(Unit target, UnitEffect effect)
    {        
        yield return null;
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
    
    public UnitEffect(UnitEffect origin)
    {
        Icon = origin.Icon;
        MaxDuration = origin.MaxDuration;
    }

    public virtual void SetPreview(UI_DmgPreview prev, Unit target) { }
    #endregion

    protected  void OnGlobalTurn(int t)
    {
        if (!Effect_Host.IsDead())
            GlobalTurnTick();
    }
   



    public UnitEffect() { }

    public virtual string GetString()
    {
        return " null";
    }

    
}