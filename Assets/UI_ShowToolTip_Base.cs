using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_ShowToolTip_Base :  MonoBehaviour  {

    protected GameObject ToolTipInstance;
    public GameObject Prefab;
    public MonoBehaviour _target;

    IToolTip Target;

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
        SpawnToolTip(_obj);
        InitializeToolTip(_obj);
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
        ToolTipInstance.GetComponent<RectTransform>().SetParent(this.transform, false);
        ToolTipInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    protected virtual void SpawnToolTip(object _obj)
    {
        ToolTipInstance = Instantiate(Prefab);
    }

    public void HideItemToolTip()
    {

        StopAllCoroutines();

        if (ToolTipInstance != null)
            ToolTipInstance.SetActive(false);
    }

}
