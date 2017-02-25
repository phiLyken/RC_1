using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UI_Menu : MonoBehaviour {

    public GameObject SquadSelection;
    public GameObject Title;
    public GameObject MainMenu;
    public GameObject TitleAnchorTop;
    public GameObject TitleAnchorBottom;

    public void ShowSquadSelection(bool show)
    {
        MainMenu.SetActive(!show);
        SquadSelection.SetActive(show);

        Sequence move = DOTween.Sequence();
        Title.transform.DOMove(show ? TitleAnchorTop.transform.position : TitleAnchorBottom.transform.position, 0.3f);

    }

	// Use this for initialization
	void Start () {

        ShowSquadSelection(MissionOutcome.LastOutcome != null);
   
	}

}
