using UnityEngine;
using System.Collections;
using System;

public class GameEndListener : MonoBehaviour {

    public static bool GameEnded = false;
    public static event Action OnMissionEnded;
    public static event Action OnMissionStarted;
    void Awake()
    {

        GameEnded = false;
    }
    // Use this for initialization
    void Start () {
        
        Unit.OnUnitKilled += CheckUnitsLeft;
        Unit.OnEvacuated += CheckUnitsLeft;

        OnMissionStarted.AttemptCall();
        TrackingManager.TrackingCall_LevelStart(GameManager.Instance.ChoosenRegionConfig, SquadManager.Instance, PlayerLevel.Instance);
	}
	
    public static void ForceMissionEnd()
    {
        EndMission();
    }
    void CheckUnitsLeft(Unit u)
    {
        int playerUnitsLeft = Unit.GetAllUnitsOfOwner(0, true).Count;
       
        if(playerUnitsLeft == 0)
        {
            EndMission();
        }
    }
    static void EndMission()
    {
        if (GameEnded)
            return;

        OnMissionEnded.AttemptCall();
        GameEnded = true;
        MissionOutcome.MakeNew();
        TrackingManager.TrackingCall_LevelComplete(GameManager.Instance.ChoosenRegionConfig, MissionOutcome.LastOutcome, PlayerLevel.Instance.GetCurrentLevel());
        ShowGameEndPopup();
    
    }
   static  void ShowGameEndPopup()
    {
        UI_Popup_Global.ShowContent(Resources.Load("UI/ui_popupcontent_game_finished_evac") as GameObject, false);
    }
    void OnDestroy()
    {
        Unit.OnUnitKilled -= CheckUnitsLeft;
        Unit.OnEvacuated -= CheckUnitsLeft;
    }
 
}
