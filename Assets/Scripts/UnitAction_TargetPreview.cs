using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class  UnitAction_TargetPreview <T>: UnitAction_TargetPreviewBase where T : Component  
{

    Dictionary<T, TargetHighLight> highlights;


    protected override void OnPreview(List<GameObject> objects)
    {
        List<T> targetables = objects.Select(obj => obj.GetComponent<T>()).ToList();
        TargetHighLight highlight_prefab = m_action.GetPreviewPrefab();

        m_action.OnTargetHover += OnTargetHover;
        m_action.OnTargetUnhover += OnTargetUnhover;

        highlights = new Dictionary<T, TargetHighLight>();

       // MDebug.Log("Preview " + objects.Count + " ");
        foreach (T obj in targetables)
        {
            TargetHighLight highlight = Instantiate(highlight_prefab).GetComponent<TargetHighLight>();
            highlight.transform.position = GetPreviewPosition(obj);
            highlights.Add(obj, highlight);
        }

    }

    void OnTargetHover(object tgt)
    {
        T target = tgt as T;

        highlights[target].Hovered.SetActive(true);
    }

    void OnTargetUnhover(object tgt)
    {
        T target = tgt as T;
        highlights[target].Hovered.SetActive(false);
    }

    protected override void OnDisable()
    {
        m_action.OnTargetHover -= OnTargetHover;
        m_action.OnTargetUnhover -= OnTargetUnhover;

        if (highlights.IsNullOrEmpty())
            return;

        foreach (var kvp in highlights)
        {
            if(kvp.Value != null && kvp.Value.gameObject != null)
                Destroy(kvp.Value.gameObject);
        }
        
        base.OnDisable();
    }

    void OnDestroy()
    {
        OnDisable();
    }
}
