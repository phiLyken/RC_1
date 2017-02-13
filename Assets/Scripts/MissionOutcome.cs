using UnityEngine;
using System.Collections;

public class MissionOutcome  {

    public static MissionOutcome LastOutcome;

    public int SuppliesGainedRaw;
    public int SuppliesGainedFinal;
    public int SquadUnitsKilled;
    public int SquadUnitsEvaced;
    public int SquadUnitsStart;
    public int RegionDifficulty;

    public float Bonus;

    public static void MakeNew()
    {
        LastOutcome = new MissionOutcome(SquadManager.Instance, PlayerLevel.Instance, PlayerInventory.Instance, GameManager.Instance);       
    }
    
    public MissionOutcome(SquadManager manager, PlayerLevel level, PlayerInventory inventory, GameManager game_manager)
    {
        SquadUnitsKilled = manager.killed.Count;
        SquadUnitsEvaced = manager.evacuated.Count;
        SuppliesGainedRaw = inventory.GetItem(ItemTypes.dust).GetCount();
        SquadUnitsStart = manager.selected_units.Count;
        RegionDifficulty = game_manager.ChoosenRegionConfig.Difficulty;

        Bonus = Constants.GetSupplyBonus(SquadUnitsStart, SquadUnitsEvaced, SquadUnitsKilled, RegionDifficulty);

        SuppliesGainedFinal = (int) ( SuppliesGainedRaw * Bonus);
    }

    public static void Resolve()
    {
        PlayerInventory.Instance.ModifyItem(ItemTypes.saved_dust, LastOutcome.SuppliesGainedFinal);
        PlayerInventory.Instance.GetItem(ItemTypes.saved_dust).SaveValue();
        PlayerLevel.Instance.AddProgress(LastOutcome.SuppliesGainedFinal);
        PlayerInventory.Instance.ModifyItem(ItemTypes.dust, - PlayerInventory.Instance.GetItem(ItemTypes.dust).GetCount());
       
    }
	
}
