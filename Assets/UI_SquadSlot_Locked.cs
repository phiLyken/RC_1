using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_SquadSlot_Locked : MonoBehaviour {

    public Text tf;

    public void SetLevel(int requirement)
    {
        tf.text = "Level " + requirement;
    }
}
