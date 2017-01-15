using UnityEngine;
using System.Collections;

public class UnitShowBlip : MonoBehaviour {

    public bool ShowDuringAbility;

    GameObject Blip;
    Unit target;


    public void Init(Unit _target, bool show_only_on_ability)
    {
        target = _target;
        ShowDuringAbility = show_only_on_ability;
        Blip = Resources.Load("Units/unit_blip").Instantiate(this.transform, true);

        Blip.SetActive(!ShowDuringAbility);


        target.Actions.OnActionStarted += CheckTurn;
        target.Actions.OnActionComplete += CheckTurn;

        target.OnIdentify += OnIdentify;
    }

    void OnIdentify(Unit triggerer)
    {
        Remove();
    }
    void Remove()
    {
        target.Actions.OnActionStarted -= CheckTurn;
        target.Actions.OnActionComplete -= CheckTurn;
        target.OnIdentify -= OnIdentify;

        Destroy(Blip);
        
    }
    void CheckTurn(UnitActionBase ability)
    {

        if(ShowDuringAbility)
        {
            Blip.SetActive(ability.ActionInProgress);
        }
    }
    void OnDestroy()
    {
        Remove();
    }
}
