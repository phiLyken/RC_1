using UnityEngine;
using System.Collections;

public class ToggleActiveOnTurn : MonoBehaviour {

    public GameObject Target;

    public void SetUnit(Unit unit)
    {

        transform.SetParent(unit.transform);
        transform.localPosition = Vector3.zero;

        Target.SetActive(false);

        unit.OnTurnStart += u =>
        {
            Target.SetActive(true);
        };

        unit.OnTurnEnded += u =>
        {
            Target.SetActive(false);
        };
    }


}
