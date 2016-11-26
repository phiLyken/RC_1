using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Util_ScaleOnEnable : MonoBehaviour {

    public UI_PopupController popup;

    public Ease ShowEase;
    public Ease CloseEase;
    public float ShowSpeed;
    public float CloseSpeed;
 

    public Vector2 MinSize;
    Vector2 start_size;

    RectTransform m_rect;

    void Awake()
    {
        if (!isActiveAndEnabled)
            return;
        if(popup != null)
        {
            popup.OnOpen += Open;
            popup.OnClose += Close;
        }

        m_rect = this.transform as RectTransform;
       
        
        start_size = m_rect.sizeDelta;
       
    }
    void OnEnable()
    {       
       if(popup == null) Open();
    }

    void Open()
    {
        m_rect.sizeDelta = MinSize;
        Sequence seq = DOTween.Sequence();
        seq.Append(m_rect.DOSizeDelta(start_size, ShowSpeed));

        if(popup != null)
        {
            seq.AppendCallback(popup.Opened );
        }
       
    }

    public void Close()
    {
        
        Sequence seq = DOTween.Sequence();
        seq.Append(m_rect.DOSizeDelta(MinSize, CloseSpeed));

        if (popup != null)
        {
            seq.AppendCallback(popup.Closed);
        }
    }

}
