using UnityEngine;
using System.Collections;

public class UI_ProgressDisplay : MonoBehaviour {

    public UI_PlayerLevel player_level;
    public UI_Bar old_bar;

    void Awake()
    {
        player_level.gameObject.SetActive(false);
        old_bar.gameObject.SetActive(false);
    }
    public void SetView(MissionOutcome outcome)
    {
        player_level.gameObject.SetActive(true);
        player_level.Init(PlayerLevel.Instance);
        old_bar.gameObject.SetActive(true);

        player_level.CurrentLevelTF.GetComponent<UI_Anim_BlinkOnEnable>().enabled = outcome.NewLevel;

        if (outcome.NewLevel)
            player_level.CurrentLevelTF.text += " [level up]";

        player_level.CurrentProgressTF.text = "+" + outcome.SuppliesGainedFinal;
       
        old_bar.enabled = !outcome.NewLevel;
        old_bar.SetProgress(outcome.ProgressBefore);
    }
}
