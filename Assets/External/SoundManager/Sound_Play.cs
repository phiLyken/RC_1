using UnityEngine;
using System.Collections;

public class Sound_Play : MonoBehaviour {

    public AudioSource Source;
    public bool PlayOneShot;
    public bool UseTaggedSource;
    public string SourceTag;

    public bool CreateListenerSource;


   protected virtual void Awake()
    {
        if (CreateListenerSource)
        {
            AudioListener listner = GameObject.FindObjectOfType<AudioListener>();
            if (listner == null)
            {
                GameObject _newobj = new GameObject("_AUDIOLISTENER");
                listner = _newobj.AddComponent<AudioListener>();
                _newobj.AddComponent<AudioSource>();
            }


            Source = listner.gameObject.GetOrAddComponent<AudioSource>();

        }
        else if (UseTaggedSource)
        {
            GameObject find = GameObject.FindGameObjectWithTag(SourceTag);
            if (find != null)
                Source = find.GetComponent<AudioSource>();
        }
    }
    public virtual void Play(AudioClip clip)
    {
        if (clip != null)
        {
            if (PlayOneShot)
            {
                GetSource().PlayOneShot(clip);
            }
            else
            {
                GetSource().clip = clip;
                GetSource().Play();
            }
        }
    }

    protected virtual AudioSource GetSource()
    {     

        if(Source == null)
        {
            Debug.LogWarning("NO AUDIO SOURCE FOR " + gameObject.name);
        }
        return Source;
    }

    
}
