using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyConfig : MonoBehaviour {

    public List<Unit> Units;

    public static Unit GetUnitPrefab()
    {
        return  MyMath.GetRandomObject((Resources.Load("enemy_config") as GameObject).GetComponent<EnemyConfig>().Units);
    }
}
