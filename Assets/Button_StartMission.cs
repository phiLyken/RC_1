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
        SquadManager.Instance.OnSelectedUpdate += Instance_OnSelectedUpdate;
        GameManager.Instance.OnRegionSet += Instance_OnRegionSet;
        CheckEnabled();
    }

    private void Instance_OnRegionSet(RegionConfigDataBase obj)
    {
        CheckEnabled();
    }

    private void Instance_OnSelectedUpdate(List<TieredUnit> obj)
    {
        CheckEnabled();
    }
        
    void OnDestroy()
    {
        SquadManager.Instance.OnSelectedUpdate -= Instance_OnSelectedUpdate;
        GameManager.Instance.OnRegionSet -= Instance_OnRegionSet;
    }
    public void StartMission()
    {
        GameManager.StartMission();
    }
}
