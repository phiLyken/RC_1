using UnityEngine;
using System.Collections;
using System;

public class Sound_PlaySingle : Sound_Play
{

    public AudioClip clip;


    public void PlayClip()
    {
        Play(clip);
    }
}
