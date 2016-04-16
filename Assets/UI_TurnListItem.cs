using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_TurnListItem : MonoBehaviour {

    public Image Frame;
    public Image Image;
    public Text TF;
    public Text OrderId;

    void Start()
    {
        Frame.enabled = false;
    }

    public void SetTurnItem(ITurn turn)
    {
        Image.color = turn.GetColor();
     //   Debug.Log(turn.HasEndedTurn());
        Frame.enabled = TurnSystem.HasTurn(turn);
       // Frame.enabled = false;
        TF.text = turn.GetID();
    }

    public void SetOrder(int id)
    {
        OrderId.text = id.ToString();
    }
}
