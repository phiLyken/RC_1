using UnityEngine;
using System.Collections;


public enum ActionButtonID
{
    attack, special_atk_1, special_atk_2, move, loot, stim, epipen
}
public class UI_ActionBar_ButtonAnchor : MonoBehaviour {

    public ActionButtonID ButtonID;
    public KeyCode HotKey;

    public UI_ActionBar_Button ButtonPrefab;

    public UI_ActionBar_Button Spawn()
    {
        GameObject newGO = Instantiate(ButtonPrefab.gameObject);
        newGO.transform.SetParent(this.transform, false);
        newGO.transform.localPosition = Vector3.zero;

        return newGO.GetComponent<UI_ActionBar_Button>();
    }
     
         
}
