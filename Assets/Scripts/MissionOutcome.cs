using UnityEngine;
using System.Collections;
using System;

public class MissionOutcome  {

    public static MissionOutcome LastOutcome;
    public static Action<MissionOutcome> OnMissionOutcomeSet;
    public int SuppliesGainedRaw;
    public int SuppliesGainedFinal;
    public int SquadUnitsKilled;
    public int SquadUnitsEvaced;
    public int SquadUnitsStart;
    public int RegionDifficulty;

   
    public bool NewLevel;
    public float ProgressBefore;

    public float Bonus;

    public static void MakeNew()
    {
        LastOutcome = new MissionOutcome(SquadManager.Instance, PlayerLevel.Instance, PlayerInventory.Instance, GameManager.Instance);       
    }
    
    public MissionOutcome(SquadManager manager, PlayerLevel level, PlayerInventory inventory, GameManager game_manager) : 
        this(manager.killed.Count, manager.evacuated.Count, inventory.GetItem(ItemTypes.dust).GetCount(), manager.selected_units.Count, game_manager.ChoosenRegionConfig.Difficulty, level.GetProgressInLevel(), level.GetCurrentLevel(), level.GetCurrentLevel())
    {
   
        PlayerInventory.Instance.ModifyItem(ItemTypes.saved_dust, LastOutcome.SuppliesGainedFinal);
        PlayerInventory.Instance.GetItem(ItemTypes.saved_dust).SaveValue();
        level.AddProgress(LastOutcome.SuppliesGainedFinal);
        PlayerInventory.Instance.ModifyItem(ItemTypes.dust, -PlayerInventory.Instance.GetItem(ItemTypes.dust).GetCount());   
       
        if(manager.evacuated.Count > 0)
        {
            game_manager.ChoosenRegionConfig.CompleteInSave();
        }

        OnMissionOutcomeSet.AttemptCall(this);
    }

    public MissionOutcome(int _killed, int _evac, int _gainedraw, int _units_start, int _difficulty, float _progress_before, int _old_level, int _new_level)
    {
    
        
        NewLevel = _old_level < _new_level;

        SquadUnitsKilled = _killed;
        SquadUnitsEvaced = _evac;
     
        SuppliesGainedRaw = (_evac > 0 ) ? _gainedraw : 0;

        SquadUnitsStart = _units_start;
        RegionDifficulty = _difficulty;
        ProgressBefore = _progress_before;
       
        Bonus = Constants.GetSupplyBonus(SquadUnitsStart, SquadUnitsEvaced, SquadUnitsKilled, RegionDifficulty);
        SuppliesGainedFinal = (int) (SuppliesGainedRaw * Bonus);    
        LastOutcome = this;
    }

	
}
