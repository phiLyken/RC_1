﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SpeechTriggerConfig : SpeechTriggerConfigSimple
{
  
    public float Chance;

    public override Speech GetSpeech()
    {
        if (M_Math.Roll(Chance))
        {
            return WeightableFactory.GetWeighted(Speeches);
        }


        return null;
    }

}