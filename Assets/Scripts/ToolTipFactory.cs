
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ToolTipFactory
{

    public static string PATH = "UI/ToolTips/ui_tooltip_";

    public static GameObject GetPrefab(TooltipTypes type, object _obj, out bool _make_generic, out string text)
    {
        _make_generic = false;
        text = "";
        //  Debug.Log(_obj.ToString());
        switch (type)
        {
            case TooltipTypes.ability_generic:
                if (_obj is UnitAction_ApplyEffectFromWeapon)
                    return LoadFromSource("ability_attack");

                if (_obj is UnitAction_ApplyEffect)
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
                return LoadFromSource("simple");


            case TooltipTypes.turn_list_item:
                if (_obj is Unit)
                {
                    return LoadFromSource("unit");
                }
                else if (_obj is WorldCrumbler)
                {
                    _make_generic = true;
                    text = "World Crumble";
                    return LoadFromSource("simple");
                }
                else if (_obj is TurnSystemMockData.TurnSystemMock)
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
        // Debug.Log("load " + PATH + _suffix);
        return Resources.Load(PATH + _suffix) as GameObject;
    }
}