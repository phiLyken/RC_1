using UnityEngine;
using System.Collections;

public class UI_ShowUnitSummary : MonoBehaviour {

    public UI_UnitListView ListViewKilled;
    public UI_UnitListView ListViewEvacuated;

    void OnEnable()
    {
        ListViewKilled.Init(SquadManager.Instance.killed);
        ListViewEvacuated.Init(SquadManager.Instance.evacuated);
        Canvas.ForceUpdateCanvases();
    }
}
