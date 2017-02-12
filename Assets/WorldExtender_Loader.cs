using UnityEngine;
using System.Collections;

public class WorldExtender_Loader : MonoBehaviour {
    
    public RegionConfigDataBase Override;
	// Use this for initialization
	void Awake () {

        RegionConfigDataBase _config = null;

        if(GameManager.Instance.ChoosenRegionConfig == null && Override)
        {
            _config = Override;
        } else if(GameManager.Instance.ChoosenRegionConfig != null)
        {
            _config = GameManager.Instance.ChoosenRegionConfig;
        }
    
        GameObject prefab;

        if (_config != null &&_config.IsTutorial)
        {
            prefab = Resources.Load("Regions/worldextender_tutorial") as GameObject;
        } else 
        {
            prefab = Resources.Load("Regions/worldextender_normal") as GameObject;
        }
        WorldExtender ext = prefab.Instantiate(transform, true).GetComponent<WorldExtender>();
        ext.SetupGame(_config);
       
	}
	

}
