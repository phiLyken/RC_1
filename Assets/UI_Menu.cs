using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class UI_Menu : MonoBehaviour {

    public GameObject SquadSelectionPrefab;

    GameObject SquadSelection;
    public GameObject Title;
    public GameObject MainMenu;
    public GameObject TitleAnchorTop;
    public GameObject TitleAnchorBottom;

    
    public void ShowSquadSelection(bool show)
    {
        MainMenu.SetActive(!show);

        if(!show && SquadSelection == null)
        {
            return;
        }
        if(SquadSelection == null)
        {
            SquadSelection = Instantiate(SquadSelectionPrefab) as GameObject;       
        }
        Debug.Log("FOO");
        SquadSelection.SetActive(show);
        Sequence move = DOTween.Sequence();
        Title.transform.DOMove(show ? TitleAnchorTop.transform.position : TitleAnchorBottom.transform.position, 0.3f);
        Title.GetComponentInChildren<Text>().text = show ? "MISSION BRIEFING" : "REDCLIFF";

    }

	// Use this for initialization
	void Start () {

        ShowSquadSelection(MissionOutcome.LastOutcome != null);
   
	}

}
