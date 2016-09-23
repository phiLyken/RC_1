using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitAction_TargetPreviewBase : MonoBehaviour {
        
    protected  UnitActionBase m_action;

    // Use this for initialization
    void Start()
    {
        m_action = GetComponent<UnitActionBase>();

        m_action.OnUnselectAction += _b =>
        {
            OnDisable();
        };

        m_action.OnTargetsFound += OnPreview;
       

    }

    protected virtual void OnPreview(List<GameObject> objects)
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual Vector3 GetPreviewPosition(object tgt)
    {
        return Vector3.zero; 
    }
}
 