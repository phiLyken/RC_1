using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Sound_PlayFromIDWeighted : Sound_Play
{

    public List<SoundIDConfigWeighted> configs;

    IDList<List<WeightedAudio>> id_clips;

    protected override void Awake()
    {
        base.Awake();
        id_clips = new IDList<List<WeightedAudio>>(configs.Cast<IDConfig<List<WeightedAudio>>>().ToList());
    }
    // Use this for initialization
    public virtual void PlayIDWeighted(string ID)
    {
        List<WeightedAudio> clips = id_clips.GetItem(ID);
        if (clips.IsNullOrEmpty())
        {
            MDebug.Log("No Audio For " + ID);
            return;
        }

        AudioClip clip = M_Weightable.GetWeighted(clips).Value;
        Play(clip);
        
    }

    
}
[System.Serializable]
public class SoundIDConfigWeighted : IDConfig<List<WeightedAudio>>
{

}

[System.Serializable]
public class WeightedAudio : GenericWeightable<AudioClip>
{

}
