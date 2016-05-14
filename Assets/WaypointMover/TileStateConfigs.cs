using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class TileStateConfigs : MonoBehaviour {

    public VisualStateConfig[] states;

  

    public static VisualStateConfig GetMaterialForstate(string state)
    {

        foreach( VisualStateConfig conf in (Resources.Load("TileStates") as GameObject).GetComponent<TileStateConfigs>().states.ToList())
        {
            if (conf.state_name == state) return conf;
        }

        Debug.LogWarning(" no config found for " + state);
        return null;
         
    }

}
