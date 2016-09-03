using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum TooltipTypes
{
    effect, turn_list_item, ability_generic, generic_text, ability_attack, weapon_class
}
public class UI_ShowToolTip_Base :  MonoBehaviour  {


    public TooltipTypes ToolTipType;
    public string DisplayText;

    protected GameObject ToolTipInstance;
 
    public MonoBehaviour _target;
    public RectTransform ToolTipAnchor;

    IEnumerator ShowToolTipDelayed()
    {
        yield return new WaitForSeconds(0.1f);
  
        SetItemToolTip( ((IToolTip)_target).GetItem() );
    }

    public void ShowItemToolTip()
    {
        StartCoroutine(ShowToolTipDelayed());
    }

    protected virtual void SetItemToolTip(object _obj)
    {
        RemoveOldTooltip();
        SpawnToolTip(ToolTipType, _obj);
       
    }

    protected virtual void RemoveOldTooltip()
    {
        if (ToolTipInstance != null)
        {
            Destroy(ToolTipInstance);
        }

    }

    protected virtual void InitializeToolTip(object _obj)
    {
        ToolTipInstance.GetComponent<UI_ToolTip_Base>().SetItem(_obj);
        ToolTipInstance.GetComponent<RectTransform>().SetParent(ToolTipAnchor, false);
        ToolTipInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    protected virtual void SpawnToolTip(TooltipTypes type, object _obj)
    {
        bool make_generic = false;
        string generic_text = "";

        ToolTipInstance = Instantiate(ToolTipFactory.GetPrefab(type, _obj, out make_generic, out generic_text));

        if (make_generic)
        {
            _obj = new GenericToolTipTarget(string.IsNullOrEmpty(generic_text) ? DisplayText : generic_text);
        }

        InitializeToolTip(_obj);
    }

 
    public void HideItemToolTip()
    {

        StopAllCoroutines();

        if (ToolTipInstance != null)
            ToolTipInstance.SetActive(false);
    }

}

public static class ToolTipFactory {

    public static string PATH = "UI/ToolTips/ui_tooltip_";

    public static GameObject GetPrefab(TooltipTypes type, object _obj, out bool _make_generic , out string text)
    {
        _make_generic = false;
        text = "";
        Debug.Log(_obj.ToString());
        switch (type)
        {
            case TooltipTypes.ability_generic:
                if (_obj is UnitAction_ApplyEffectFromWeapon)
                    return LoadFromSource("ability_attack");

                if ( _obj is UnitAction_ApplyEffect )
                    return LoadFromSource("ability_apply_effect");

                if (_obj is UnitActionBase)
                    return LoadFromSource("ability");

                
                goto default;


            case TooltipTypes.weapon_class:
                return LoadFromSource("weapon");

            case TooltipTypes.effect:
                return LoadFromSource("effect");

            case TooltipTypes.generic_text:
                _make_generic = true;
                return LoadFromSource("effect");


            case TooltipTypes.turn_list_item:
                if(_obj is Unit  )
                {
                    return LoadFromSource("unit");
                } else if(_obj is WorldCrumbler )
                {
                    _make_generic = true;
                    text = "World Crumble";
                    return LoadFromSource("simple");
                } else if( _obj is TurnSystemMockData.TurnSystemMock)
                {
                    _make_generic = true;
                    text = (_obj as TurnSystemMockData.TurnSystemMock).GetID();
                    return LoadFromSource("simple");
                }
                goto default;

            default:
                _make_generic = true;
                text = "you should define a tooltip\ntype in this ui element";
                return LoadFromSource("simple");



        }
      
    }

    static GameObject LoadFromSource(string _suffix)
    {
        Debug.Log("load " + PATH + _suffix);
        return Resources.Load(PATH + _suffix) as GameObject;
    }
}