using UnityEngine;
using System.Collections;
using System;

public class Sound_Controller : MonoBehaviour {

    public bool MoveToListener;
    AudioListener listener;

    public bool PlayOnSoundEnable;
    public bool PauseOnSoundDisable;

    public bool SFX;
    public bool Music;

    public Action OnPlay;
    public Action OnPause;
    public Action OnStop;
    public AudioSource Source;

    float start_volume;
    float volume_modifier;
    float settings_volume;

    void Awake()
    {
        volume_modifier = 1;
        start_volume = Source.volume;
        
        if (Music)
        {
            settings_volume = SoundManager.Instance.Volume_Music;
            SoundManager.Instance.OnMusicVolumeChanged += OnVolumeChanged;
        }

        if (SFX)
        {
            SoundManager.Instance.OnSfxVolumeChanged += OnVolumeChanged;
            settings_volume = SoundManager.Instance.Volume_SFX;
        }
        UpdateVolume();
    }

    void Update()
    {
        if(MoveToListener )
        {
            if(listener == null)
               listener = GameObject.FindObjectOfType<AudioListener>();

            if(listener != null)
            {
                Source.transform.position  = listener.gameObject.transform.position;               
            }
        }
    }

    void OnVolumeChanged(float f)
    {
        if(Source.playOnAwake && PlayOnSoundEnable && Source.volume == 0 && f > 0 && !Source.isPlaying)
        {
            OnPlay.AttemptCall();
            Play();
        }

        if(PauseOnSoundDisable && Source.volume > 0 && f == 0 && Source.isPlaying){
            OnPause.AttemptCall();
            Pause();
            
        }

        if(!PauseOnSoundDisable && f == 0)
        {
            OnStop.AttemptCall();
            Stop();
        }

        settings_volume = f;
        UpdateVolume();

    }

    void UpdateVolume()
    {
        Source.volume = settings_volume * start_volume * volume_modifier;
    }
    public virtual void SetVolumeMultiplier(float f)
    {
        volume_modifier = f;
        UpdateVolume();
    }
    public virtual void Play()
    {
        Source.Play();
    }

    public virtual void Stop()
    {
        Source.Stop();
    }

    public virtual void Pause()
    {
        Source.Pause();
    }

    void OnDestroy()
    {
        if (Music)
        {            
            SoundManager.Instance.OnMusicVolumeChanged -= OnVolumeChanged;
        }
        if (SFX)
        {
            SoundManager.Instance.OnSfxVolumeChanged -= OnVolumeChanged;
        }
    }
}
