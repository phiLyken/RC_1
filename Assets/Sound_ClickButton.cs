using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sound_ClickButton : Sound_Play {

    public Button Button;
    public SoundManager.Presets preset;



    protected override void Awake()
    {
        UseTaggedSource = true;
        SourceTag = "SoundSFXSource";
        PlayOneShot = true;

        base.Awake();
    }
    void Start()
    {
        if(Button == null)
        {
          Button=   GetComponent<Button>();
        }
        Button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        Play( SoundManager.GetClip(preset));
    }
    



}


