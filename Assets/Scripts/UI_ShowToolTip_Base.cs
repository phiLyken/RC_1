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
 
    protected UI_ToolTip_Base ToolTipInstance;
 
    public MonoBehaviour _target;
    public RectTransform ToolTipAnchor;

    IEnumerator ShowToolTipDelayed()
    {
        yield return new WaitForSeconds(0.4f);
        object target = null;;
        if(_target != null) target = ((IToolTip) _target).GetItem();
        SetItemToolTip(target);
    }

    IEnumerator HideToolTipDelayed()
    {
        
        yield return null;
        RemoveOldTooltip();

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
        if (ToolTipInstance != null && ToolTipInstance.AttemptHide() )
        {
            Destroy(ToolTipInstance.gameObject);
        }

    }

    protected virtual void InitializeToolTip(object _obj)
    {
        ToolTipInstance.GetComponent<UI_ToolTip_Base>().SetItem(_obj);

        UI_ToolTip_Base parent = gameObject.GetComponent<UI_ToolTip_Base>();

        if (parent == null)
            parent = gameObject.GetComponentInParent<UI_ToolTip_Base>();


        if (parent != null)
            parent.RegisterChild(ToolTipInstance.GetComponent<UI_ToolTip_Base>());

       
        ToolTipInstance.transform.SetParent(GameObject.FindWithTag("UI_Overlay").transform, true);
       
        ToolTipInstance.GetComponent<RectTransform>().position = ToolTipAnchor.position ;
    }

    protected virtual void SpawnToolTip(TooltipTypes type, object _obj)
    {
        bool make_generic = false;
        string generic_text = "";

        ToolTipInstance = Instantiate(ToolTipFactory.GetPrefab(type, _obj, out make_generic, out generic_text)).GetComponent<UI_ToolTip_Base>();

        if (make_generic)
        {
            _obj = new GenericToolTipTarget(string.IsNullOrEmpty(generic_text) ? DisplayText : generic_text);
        }

        InitializeToolTip(_obj);
    }

 
    public void HideItemToolTip()
    {

        StopAllCoroutines();
        StartCoroutine(HideToolTipDelayed());

    }

}

