using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Button_StartMission : MonoBehaviour {

    public GameObject SquadSelectionHint;
    public GameObject RegionSelectedHint;
    void CheckEnabled()
    {

        if(SquadManager.Instance.selected_units.Count == 0 && PlayerPrefs.GetInt(Constants.TUTORIAL_SAVE_ID) == 1)
        {
            SquadSelectionHint.SetActive(true);
        } else
        {
            SquadSelectionHint.SetActive(false);
        }
 
        RegionSelectedHint.SetActive(GameManager.Instance.ChoosenRegionConfig == null);

        GetComponent<Button>().interactable =  GameManager.Instance.ChoosenRegionConfig != null && ( GameManager.Instance.ChoosenRegionConfig.IsTutorial || SquadManager.Instance.selected_units.Count > 0 );

    }

    void OnEnable()
    {
        SquadManager.Instance.OnSelectedUpdate += OnSelectedUpdate;
        GameManager.Instance.OnRegionSet += OnRegionSet;
        CheckEnabled();
    }

    private void OnRegionSet(RegionConfigDataBase obj)
    {
        CheckEnabled();
    }

    private void OnSelectedUpdate(List<TieredUnit> obj)
    {
        CheckEnabled();
    }
        
    void OnDisable()
    {
        SquadManager.Instance.OnSelectedUpdate -= OnSelectedUpdate;
        GameManager.Instance.OnRegionSet -= OnRegionSet;
    }
    public void StartMission()
    {
        GameManager.StartMission();
    }
}
