using UnityEngine;
using System.Collections;
using System;

public class UI_WorldPos : MonoBehaviour {
    public Vector2 WorldPositionOffset;
    public Transform worldPosAnchor;
    Vector3 worldPos;




    public void SetWorldPosition(Vector3 pos)
    {
        RectTransform m_rect = GetComponent<RectTransform>();
        m_rect.localPosition = M_Math.WorldToCanvasPosition(transform.parent.GetComponent<RectTransform>(), Camera.main, pos, WorldPositionOffset);
    }

    public void SetWorldPosObject(Transform tr)
    {
        worldPosAnchor = tr;
        UpdatePos();
    }

    public void UpdatePos()
    {
        if(worldPosAnchor != null)
        {
            worldPos = worldPosAnchor.position;
        }

        SetWorldPosition(worldPos);
    }
   

}
