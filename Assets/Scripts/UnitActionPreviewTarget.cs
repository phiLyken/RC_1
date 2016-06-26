using UnityEngine;
using System.Collections;

public class UnitActionPreviewTarget : MonoBehaviour {

    public GameObject IndicatorPrefab;
    public bool UnitTarget;
    public bool TileTarget;

    GameObject AimIndicator;
    Transform target;
    UnitActionBase m_action;

    // Use this for initialization
    void Start () {
        AimIndicator = Instantiate(IndicatorPrefab);
        AimIndicator.transform.SetParent(transform, true);
        AimIndicator.SetActive(false);
        m_action = GetComponent<UnitActionBase>();

        m_action.OnUnselectAction += _b =>
        {
            DisableIndicator(null);
        };

        m_action.OnTargetHover += t =>
        {
            if(UnitTarget)
                ShowIndicator((t as Unit).transform);
            if (TileTarget)
                ShowIndicator((t as Tile).transform);
        };

        m_action.OnTargetUnhover += t =>
        {
            DisableIndicator(null);
        };

    }
    
    void ShowIndicator(Transform tr)
    {
        target = tr;
        AimIndicator.SetActive(true);
    }

    void DisableIndicator(Transform tr)
    {
        target = null;
        AimIndicator.SetActive(false);
    }

    void Update()
    {
        if(target != null)
        {
            AimIndicator.transform.position = target.position;
        }
    }

    void OnDestroy()
    {
        Destroy(AimIndicator);
    }
}
