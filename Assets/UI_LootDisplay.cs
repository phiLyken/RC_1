using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_LootDisplay : MonoBehaviour {

    public Text Looted;
    public GameObject LootedDescr;

    public Text BonusTF;
    public GameObject BonusDescr;

    public Text SurvivedTF;


    public Text FinalLootTF;
    public GameObject FinalLootDescr;

    public GameObject NoLoot;

    public void SetView(MissionOutcome outcome, float delay)
    {
        LootedDescr.SetActive(false);
        Looted.gameObject.SetActive(false);
        BonusDescr.SetActive(false);
        BonusTF.gameObject.SetActive(false);

        SurvivedTF.gameObject.SetActive(false);
        FinalLootDescr.SetActive(false);
        FinalLootTF.gameObject.SetActive(false);
              
        SurvivedTF.text = "(" + outcome.SquadUnitsEvaced.ToString() + " Survived)";
               
        NoLoot.SetActive(outcome.SquadUnitsEvaced == 0);

        if(outcome.SquadUnitsEvaced > 0)
        {
            StopAllCoroutines();
            StartCoroutine(Sequnce(outcome, delay));
        }
    }

    IEnumerator Sequnce(MissionOutcome outcome, float delay)
    {

        yield return new WaitForSeconds(0.5f+ delay);

        LootedDescr.SetActive(true);
        Looted.gameObject.SetActive(true);
        Looted.text = outcome.SuppliesGainedRaw.ToString();

        yield return new WaitForSeconds(1f);
        SurvivedTF.gameObject.SetActive(true);
        BonusTF.gameObject.SetActive(true);
        BonusTF.text = (outcome.Bonus).ToString("#%");
        BonusDescr.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        FinalLootDescr.SetActive(true);
        FinalLootTF.text = outcome.SuppliesGainedFinal.ToString();
        FinalLootTF.gameObject.SetActive(true);


        yield break;


    }
}
