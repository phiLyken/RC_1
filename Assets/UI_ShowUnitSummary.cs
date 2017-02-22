using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ShowUnitSummary : MonoBehaviour {

    public UI_LootDisplay Loot;
    public UI_ProgressDisplay Progress;
    
    public UI_UnitListView ListViewEvacuated;

    public Button BackButton;

    void OnEnable()
    {
        BackButton.interactable = false;
        ListViewEvacuated.Init(SquadManager.Instance.evacuated);
        Canvas.ForceUpdateCanvases();
        Loot.SetView(MissionOutcome.LastOutcome,0);

        //disable delay if there are no rewards (delay is reserving time for loot view)
        M_Extensions.ExecuteDelayed(this, MissionOutcome.LastOutcome.SquadUnitsEvaced == 0 ? 0 : 5, () => Progress.SetView(MissionOutcome.LastOutcome));
        M_Extensions.ExecuteDelayed(this, MissionOutcome.LastOutcome.SquadUnitsEvaced == 0 ? 0 : 5.25f, () => { BackButton.interactable = true; });
       
       
    }
 
}
