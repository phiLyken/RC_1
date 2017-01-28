using UnityEngine;
using System.Collections;

public class UnitShowBlip : MonoBehaviour {


    public BlipBehaviour Behavior;
    GameObject Blip;
    Unit target;


    public void Init(Unit _target, ScriptableUnitConfig config)
    {
        target = _target;

        Behavior = config.BlipBehavior;
        Blip = Resources.Load("Units/unit_blip").Instantiate(this.transform, true);

        Blip.SetActive(Behavior == BlipBehaviour.always);

        if(Behavior == BlipBehaviour.on_action)
        { 
            target.Actions.OnActionStarted += CheckTurn;
            target.Actions.OnActionComplete += CheckTurn;
        }

        target.OnIdentify += OnIdentify;
    }

    void OnIdentify(Unit triggerer)
    {
        Remove();
    }

    void Remove()
    {
        if (Behavior == BlipBehaviour.on_action)
        {
            target.Actions.OnActionStarted -= CheckTurn;
            target.Actions.OnActionComplete -= CheckTurn;
        }
        target.OnIdentify -= OnIdentify;

        Destroy(Blip);        
    }

    void CheckTurn(UnitActionBase ability)
    {
        if(Behavior == BlipBehaviour.on_action)
        {
            Blip.SetActive(ability.ActionInProgress);
        }
    }
    void OnDestroy()
    {
        Remove();
    }
}
