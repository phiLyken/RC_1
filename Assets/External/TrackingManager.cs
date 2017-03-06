using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class TrackingManager  {



    public static void TrackingCall_LevelComplete(RegionConfigDataBase level, MissionOutcome outcome, int player_level )
    {
        Dictionary<string, object> call = new Dictionary<string, object>();

        call["player_level"] = player_level;
        call["supplies"] = outcome.SuppliesGainedFinal;
        call["evacuated"] = outcome.SquadUnitsEvaced;
        call["killed"] = outcome.SquadUnitsKilled;
        call["started"] = outcome.SquadUnitsStart;
        
        call["editor"] = Application.isEditor.ToString();
        

        Analytics.CustomEvent("mission_end_"+level.name, call);
    }

    public static void TrackingCall_LevelStart(RegionConfigDataBase region, SquadManager squad, PlayerLevel level)
    {
        Dictionary<string, object> call = new Dictionary<string, object>();

        call["player_level"] = level.GetCurrentLevel();
       
        call["int"] = squad.selected_units;
        call["cleared"] = region.IsCompleteInSave().ToString();

        call["editor"] = Application.isEditor.ToString();


        Analytics.CustomEvent("mission_start_" + region.name, call);
    }

    
}
