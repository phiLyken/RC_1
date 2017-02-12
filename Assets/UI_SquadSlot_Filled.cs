using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_SquadSlot_Filled : MonoBehaviour {
    public Text TF;
    public Image Portrait;

    public void Set(ScriptableUnitConfig c)
    {
        TF.text = c.ID;
        Portrait.sprite = c.MeshConfig.HeadConfig.Heads[0].UI_Texture;
    }
}