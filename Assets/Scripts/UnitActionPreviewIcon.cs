using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitActionPreviewIcon : MonoBehaviour {

    
    UnitActionBase m_action;

    // Use this for initialization
    void Start()
    {
        m_action = GetComponent<UnitActionBase>();

        m_action.OnUnselectAction += _b =>
        {
            UI_ActionPreviewIcon.Disable();
        };

        m_action.OnTargetsFound += _list =>
        {
            List<Transform> targets = _list.Select(i => i.transform).ToList();
            UI_ActionPreviewIcon.PreviewOnTargets(targets, m_action.GetImage());
            
        };

    }




}
 