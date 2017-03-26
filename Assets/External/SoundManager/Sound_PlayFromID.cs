using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Sound_PlayFromID : Sound_Play {

    public List<SoundIDConfig> configs;
    
    IDList<AudioClip> id_clips;

    protected override void Awake()
    {
        base.Awake();
        id_clips = new IDList<AudioClip>(configs.Cast<IDConfig<AudioClip>>().ToList());
    }
    // Use this for initialization
    public virtual void PlayID(string ID)
    {
        AudioClip clip = id_clips.GetItem(ID);
        Play(clip);
    }

    
}


[System.Serializable]
public class SoundIDConfig : IDConfig<AudioClip>
{
}
