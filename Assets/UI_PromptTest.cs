using UnityEngine;
using System.Collections;

public class UI_PromptTest : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
        UI_Prompt.MakePrompt(this.transform as RectTransform, "TEST OMG", 2, CanRemove, true);
	}
	
    bool CanRemove()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

}
