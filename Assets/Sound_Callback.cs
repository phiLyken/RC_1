using UnityEngine;
using System.Collections;

public class Sound_Callback : MonoBehaviour {

    public void SoundCallback(string ID)
    {
        GetComponent<Sound_PlayFromID>().PlayID(ID);
    }

    public void SoundCallback()
    {
        GetComponent<Sound_PlaySingle>();
    }
}
