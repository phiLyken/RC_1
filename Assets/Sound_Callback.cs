using UnityEngine;
using System.Collections;

public class Sound_Callback : MonoBehaviour {

    Sound_PlaySingle single;
    Sound_PlayFromID id;
    Sound_PlayFromIDWeighted id_weighted;

    void Start()
    {
        single = GetComponent<Sound_PlaySingle>();
        id = GetComponent<Sound_PlayFromID>();
        id_weighted = GetComponent<Sound_PlayFromIDWeighted>();

    }
    public void SoundCallback(string ID)
    {
        if (id != null)
        {
            id.PlayID(ID);
        }

        if(id_weighted!= null)
        {
            id_weighted.PlayIDWeighted(ID);
        }

    }

    public void SoundCallback()
    {
        if (single != null)
        {
            single.PlayClip();
        }
    }
}
