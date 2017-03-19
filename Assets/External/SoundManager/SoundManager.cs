using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class SoundManager : MonoBehaviour, IInit
{
    public enum Presets
    {
        menu_button_close, menu_button_click, menu_button_select, error, ability_confirm, ability_cancel, ability_select
    }

    static string SFX_ENABLED_SAVE_ID = "_sound_sfx";
    static string MUSIC_ENABLED_SAVE_ID = "_sound_music";

    public Action<float> OnSfxVolumeChanged;
    public Action<float> OnMusicVolumeChanged;

    bool sfx_enabled;
    bool music_enabled;

    public float Volume_SFX;
    public float Volume_Music;

    static SoundManager _instance;

    public static void PlayAtSourceTag(string SourceTag, string clip)
    {
        GameObject find = GameObject.FindGameObjectWithTag(SourceTag);
        if (find != null)
            find.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Sounds/" + clip) as AudioClip);
        ;

    }

    public static SoundManager Instance
    {
        get { return _instance == null ? M_Extensions.MakeMonoSingleton<SoundManager>(out _instance) : _instance; }
    }

    public static AudioClip GetClip(Presets preset)
    {

        return Resources.Load("Sounds/sfx_" + preset.ToString()) as AudioClip;

    }

    public void Init()
    {
        sfx_enabled =  PlayerPrefs.GetInt(SFX_ENABLED_SAVE_ID, 1) == 1;
        music_enabled = PlayerPrefs.GetInt(MUSIC_ENABLED_SAVE_ID, 1) == 1;

        SetSFX(sfx_enabled);
        SetMusic(music_enabled);
    }

    public static void SetSFX(bool enabled)
    {
        PlayerPrefs.SetInt(SFX_ENABLED_SAVE_ID, enabled ? 1 : 0);
        SetSFX(enabled ? 1 : 0);
    }

    public static void SetMusic(bool enabled)
    {
        PlayerPrefs.SetInt(MUSIC_ENABLED_SAVE_ID, enabled ? 1 : 0);
        SetMusic(enabled ? 1 : 0);
    }

    public static void SetMusic(float value)
    {

        Instance.Volume_Music = value;
        Instance.OnMusicVolumeChanged.AttemptCall(value);
    }
    public static void SetSFX(float value)
    {
        Instance.Volume_SFX = value;
        Instance.OnSfxVolumeChanged.AttemptCall(value);
    }
    public static float GetSFXVolume()
    {
        return  Instance.sfx_enabled ? 1 : 0;
    }

    public static float GetMusicVolume()
    {
        return Instance.music_enabled ? 1 : 0;
    }

    void OnDestroy()
    {
        if (_instance == null)
            return;

        Instance.OnMusicVolumeChanged = null;
        Instance.OnSfxVolumeChanged = null;
    }

    public static Sound_Play PlaySFX(AudioClip clip, Transform target)
    {

        GameObject oneshot_source = GameObject.Instantiate(Resources.Load("Sounds/sfx_oneshot")) as GameObject;
        Sound_Play play = oneshot_source.GetComponent<Sound_Play>();

        oneshot_source.transform.position = target.transform.position;        

        Sequence seq = DOTween.Sequence();
      
        seq.AppendInterval(clip.length);
        seq.AppendCallback(() => GameObject.Destroy(oneshot_source));

        play.Play(clip);
        return play;

        
    }
}
