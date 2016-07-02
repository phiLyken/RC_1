using UnityEngine;
using System.Collections;

public class UnitTestSpawner : MonoBehaviour {

    public ScriptableUnitConfig Config;

    void Start()
    {
        if(Config != null)
        {
            UnitSpawner spawner = gameObject.AddComponent<UnitSpawner>();
            spawner.SpawnUnit(Config, 0, 0);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(Config != null)
          MyMath.SceneViewText(Config.ID, transform.position);
    }
#endif
}
