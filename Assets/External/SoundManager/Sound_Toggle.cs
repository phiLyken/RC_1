using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sound_Toggle : MonoBehaviour {

    public Toggle Toggle;
    
    public bool SFX;
    public bool Music;

	// Use this for initialization
	void Start () {

        if (Music)
        {
           SoundManager.Instance.OnMusicVolumeChanged += CheckMusic;
            Toggle.isOn = SoundManager.GetMusicVolume() > 0;
            
        }

        if (SFX)
        {
            Toggle.isOn = SoundManager.GetSFXVolume() > 0;
            SoundManager.Instance.OnSfxVolumeChanged += CheckSFX;
        }
    }

    void CheckMusic(float v)
    {
 
        if(Music)
            Toggle.isOn = v > 0;
    }

    void CheckSFX(float v)
    {
        if (SFX)
            Toggle.isOn = v > 0;
    }

    public void SetToggle()
    {
 

        if (SFX)
        {
            SoundManager.SetSFX(Toggle.isOn);
        }

        if (Music)
        {
            SoundManager.SetMusic(Toggle.isOn);
        }
    }

    void OnDestroy()
    {
        if (Music)
        {
            SoundManager.Instance.OnMusicVolumeChanged -= CheckMusic;
        

        }

        if (SFX)
        {
         
            SoundManager.Instance.OnSfxVolumeChanged -= CheckSFX;
        }
    }
}
