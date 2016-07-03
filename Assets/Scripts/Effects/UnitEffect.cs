using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UnitEffect
{
    public Sprite Icon;
    public GameObject ApplyEffectOnTargetPrefab;
    public GameObject CastEffectOnInstigatorPrefab;
    public bool MakeLazer;
    public Color LazerColor;

    public bool FocusOnCaster;
    public bool FocusOnTarget;
    public float castDelay;

    public int TurnLength;

    public virtual IEnumerator ApplyEffectSequence(Unit target, Unit instigator) {

        if (FocusOnCaster && PanCamera.Instance != null) {
            PanCamera.Instance.PanToPos(instigator.currentTile.GetPosition());
            yield return new WaitForSeconds(0.5f);
        }

        if(ApplyEffectOnTargetPrefab != null) {
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

    ///OVERIDE THIS
    protected virtual IEnumerator ApplyEffect(Unit target, UnitEffect effect)
    {
        yield return null;
    }
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


}