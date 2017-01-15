using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UI_TurnListItem : MonoBehaviour, IToolTip {

    public Image BackGround;
    public Image Image;
   
    public Text OrderId;

    ITurn m_turn;

    public ITurn GetTurnable()
    {
        return m_turn;

    }
    public GameObject ActiveTurnIndicator;
    public GameObject MouseOverIndicator;

    public Action<ITurn> OnSetTurnAble;
     

    public void SetTurnItem(ITurn turn)
    {
        m_turn = turn;
        if (OnSetTurnAble != null) OnSetTurnAble(m_turn);

      //  Image.color = turn.GetColor();
        Image.sprite = turn.GetIcon();
        BackGround.color = turn.GetColor();

        ActiveTurnIndicator.GetComponent<Image>().color = Color.white;
        UpdateActiveTurnIndicator( );

        turn.OnUpdateSprite += () => SetTurnItem(turn);
        
    }

    public void UpdateActiveTurnIndicator()
    {
        ActiveTurnIndicator.SetActive(TurnSystem.HasTurn(m_turn));
    }


    public void SetOrder(int id)
    {
        OrderId.text = id.ToString();
    }

    public object GetItem()
    {
        return m_turn;
    }
    
    public void Remove()
    {
        Sequence anim = DOTween.Sequence();
        anim.Append(transform.DOMove((transform as RectTransform).position - Vector3.up * (transform as RectTransform).sizeDelta.y, 0.5f));
        anim.Append(GetComponent<CanvasGroup>().DOFade(0, 0.5f));
        anim.AppendCallback(() => { Destroy(this.gameObject); });
        anim.Play();
    }
}
