using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_Prompt : MonoBehaviour {

    public Text tf;
    public GameObject anchor;

    Func<bool> CanRemove;
    Transform follow;

    public void Init(string Text, int ArrowPos,  Func<bool> canRemove, bool ShowButton)
    {
        TurnSystem.Instance.OnStartTurn += OnTurnStart;
        CanRemove = canRemove;
        GetComponentInChildren<CompasGroup>().SelectIndex(ArrowPos);         
        GetComponentInChildren<Button>().gameObject.SetActive(ShowButton);

        SetText(Text);
    }

    public void SetText(string text)
    {
        tf.text = text;

    }

    void OnTurnStart(ITurn turn)
    {
        
        if(turn as Unit != null && (turn as Unit).OwnerID == 0)
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {

        if (CanRemove != null && CanRemove())
        {
            Destroy(this.gameObject);
        }
    }

    public static UI_Prompt MakePrompt(RectTransform anchor_to, string Text, int ArrowPos, Func<bool> canRemove, bool ShowButton)
    {
        GameObject obj = Instantiate(Resources.Load("UI/ui_prompt")) as GameObject;

        obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI_Overlay").transform, false);

        UI_Prompt prompt = obj.GetComponent<UI_Prompt>();

        (obj.transform as RectTransform).position = anchor_to.position;

       
        prompt.Init(Text, ArrowPos, canRemove, ShowButton);

        return prompt;
    }

    void OnDestroy()
    {
        TurnSystem.Instance.OnStartTurn -= OnTurnStart;
    }

}
