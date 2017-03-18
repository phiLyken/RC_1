using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class Sound_Singleton : Sound_Play {

    public List<SoundIDConfig> SceneConfigs;

    AudioClip currentClip;

    protected IDList<AudioClip> idList;

    static Sound_Singleton _instance;
    

    protected override void Awake()
    {
        base.Awake();

        if(_instance == null)
        {
            idList = new IDList<AudioClip>(SceneConfigs.Cast<IDConfig<AudioClip>>().ToList());
            _instance = this;
            DontDestroyOnLoad(this);           
           
        }
        if (_instance == this)
        {
            PlaySoundForScene(SceneManager.GetActiveScene().name);
        } else
        {
            Destroy(this);
        }


    }
    public override void Play(AudioClip clip)
    {
        if(currentClip != clip)
        {
            base.Play(clip);
        }
        currentClip = clip;
       
    }

    protected virtual void PlaySoundForScene(string current_scene)
    {
        if (_instance == null || _instance != this)
            return;

        if (idList.HasID(current_scene))
        {
            if (currentClip == null || idList.GetItem(current_scene) != currentClip)
            {
                currentClip = idList.GetItem(current_scene);
                GetSource().clip = currentClip;            
            } 

            if (!GetSource().isPlaying)
            {
                GetSource().Play();
            }  
         
        }
        else
        {
            GetSource().Stop();
        }
    }
    
    protected virtual void OnNewLevel()
    {
        if (_instance == null || _instance != this)
            return;

        PlaySoundForScene(SceneManager.GetActiveScene().name);
    }

    void OnLevelWasLoaded(int level)
    {
        OnNewLevel();
    }

     
 
}
