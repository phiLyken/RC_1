using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_TurnListItem : MonoBehaviour {

    public Image Frame;
    public Image Image;
    public Text TF;
    public Text OrderId;

    ITurn m_turn;
    public ITurn GetTurnable()
    {
        return m_turn;

    }
    public GameObject ActiveIndicator;

    public TurnableEventHandler OnSetTurnAble;

    void Start()
    {
        Frame.enabled = false;
        SetActiveIndicator(false);
    }
    public void SetActiveIndicator(bool enabled)
    {
        ActiveIndicator.SetActive(enabled);
    }
    public void SetTurnItem(ITurn turn)
    {
        m_turn = turn;
        if (OnSetTurnAble != null) OnSetTurnAble(m_turn);

        Image.color = turn.GetColor();
     //   Debug.Log(turn.HasEndedTurn());
        Frame.enabled = TurnSystem.HasTurn(turn);
       // Frame.enabled = false;
        TF.text = turn.GetID();
        SetActiveIndicator(false);
    }

    public void SetOrder(int id)
    {
        OrderId.text = id.ToString();
    }

   
}
