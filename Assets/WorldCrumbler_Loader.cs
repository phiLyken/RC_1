using UnityEngine;
using System.Collections;

public class WorldCrumbler_Loader : MonoBehaviour {

    public bool useOVerride;
   public WorldCrumbler Override;

    void Awake()
    {
        WorldCrumbler crumbler_prefab;

        if (useOVerride && Override != null)
        {
            crumbler_prefab = Override;
        } else if(GameManager.Instance.ChoosenRegionConfig != null)
        {
            crumbler_prefab = GameManager.Instance.ChoosenRegionConfig.CrumblerSetting;
        } else
        {
            crumbler_prefab = (Resources.Load("Regions/worldcrumbler_normal") as GameObject).GetComponent<WorldCrumbler>();
        }
       
        WorldCrumbler ext = crumbler_prefab.gameObject.Instantiate(transform, true).GetComponent<WorldCrumbler>();
        ext.Init(GameObject.FindObjectOfType<TurnSystem>());
    }

}
