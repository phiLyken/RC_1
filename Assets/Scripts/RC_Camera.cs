using UnityEngine;
using System.Collections;

public class RC_Camera : StrategyCamera {
        
    void Awake()
    {
        Unit.OnTurnStart += StartTurn;
    }

    void StartTurn(Unit u)
    {
        if(u.IsIdentified)
            ActionPanToPos.GoTo(u.transform.position);
    }

    public void OnDestroy()
    {
        Unit.OnTurnStart -= StartTurn;
    }
}
