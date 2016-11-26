using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class UI_TriggerButtonOnKey : MonoBehaviour {

    public KeyCode InputKey;
    
 
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf && Input.GetKeyDown(InputKey))
        {
            Send();
        }
	}

    void Send()
    {
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }
}
