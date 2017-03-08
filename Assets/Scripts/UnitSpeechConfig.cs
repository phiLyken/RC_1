using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class UnitSpeechConfig :ScriptableObject
{

    [Header("Normal Configs")]
    public SpeechTriggerConfig Died;
    public SpeechTriggerConfig GainAdrRush;
    public SpeechTriggerConfig ReceiveDamageSmall;
    public SpeechTriggerConfig ReceiveDamageBig;
    public SpeechTriggerConfig Turn;
    public SpeechTriggerConfig Identify;

    [Header("Configs no chance")]
    public SpeechTriggerConfig Crumble;
    public SpeechTriggerConfig FriendDie;
    public SpeechTriggerConfig FoeDie;
    public SpeechTriggerConfig Selected;


    [Header("ID Configs")]
    public SpeechTriggerConfigID UseAbility;
    public SpeechTriggerConfigID AppliedEffect;
    public SpeechTriggerConfigID ReceiveEffect;
     
}




[Serializable]
public class Speech : IWeightable
{
    public string[] Lines = new string[1];

    public float _Weight = 1;

    float IWeightable.Weight
    {
        get
        {
            return _Weight;
        }

        set
        {
            _Weight = value;
        }
    }
     
 
}
