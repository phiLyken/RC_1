using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void ChoiceCallBack(int choice);
public class UI_Choice : MonoBehaviour {

    int choice = -1;
    ChoiceCallBack callback;

    public void WaitForChoice(string[] texts, ChoiceCallBack cb)
    {
        choice = -1;
        Button[] buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < texts.Length)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<Text>().text = texts[i];

            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        callback = cb;
    }
    public static void CreateUIChoice(string[] texts, ChoiceCallBack cb)
    {
        GameObject ui = (Instantiate(Resources.Load("LootUI") as GameObject));
        ui.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false) ;
        ui.GetComponent<UI_Choice>().WaitForChoice( texts, cb);
    }

    public void MakeChoice(int choice)
    {
        callback(choice);
    }
}
