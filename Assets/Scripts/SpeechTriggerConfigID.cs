﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SpeechTriggerConfigID : ScriptableObject
{   
    public List<SpeechTriggerConfigIDGroupConfig> Groups;
    public virtual Speech GetSpeech(string trigger)
    {
        SpeechTriggerConfigIDGroupConfig _speech = Groups.FirstOrDefault(gr => gr.ID == trigger);

        if (_speech != null && M_Math.Roll(_speech.Chance))
        {
            return M_Weightable.GetWeighted(_speech.Speeches);
        }

        return null;
    }
}

[System.Serializable]
public class SpeechTriggerConfigIDGroupConfig
{
    public float Chance;
    public string ID;
    public List<Speech> Speeches;
}