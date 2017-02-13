using UnityEngine;
using System.Collections;

public class Button_EndMission : MonoBehaviour {

    public void EndMission()
    {
        GameManager.GoToSquad();
    }
}
