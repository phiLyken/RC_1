using UnityEngine;
using System.Collections;

public class UnitTestSpawner : MonoBehaviour {

    public ScriptableUnitConfig Config;

    void Start()
    {
        if(Config != null)
        {
            UnitSpawner spawner = gameObject.AddComponent<UnitSpawner>();
            spawner.SpawnUnit(Config, new M_Math.R_Range(1,5), 0, true);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(Config != null)
          M_Math.SceneViewText(Config.ID, transform.position);
    }
#endif
}
