using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ShowGameOverScore : MonoBehaviour {

    public UI_Resource resource_view;
    public Text TF;

    void OnEnable()
    {
        resource_view.SetCount(MissionOutcome.LastOutcome.SuppliesGainedFinal);
       // TF.text = string.Format("Altough we regret the loss of Unit-{0}.We appreciate your efforts to secure precious supplies.", (Random.Range(0, 10) + Random.Range(0, 10)).ToString());
    }
}

