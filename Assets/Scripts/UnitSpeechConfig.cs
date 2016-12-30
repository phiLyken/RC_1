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

    [Header("Simple Configs")]
    public SpeechTriggerConfigSimple Crumble;
    public SpeechTriggerConfigSimple FriendDie;
    public SpeechTriggerConfigSimple FoeDie;
    public SpeechTriggerConfigSimple Selected;


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
