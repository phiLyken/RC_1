using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;



public class SpeechTriggerConfigSimple : ScriptableObject
{
    public List<Speech> Speeches;

    public virtual Speech GetSpeech()
    {
      
            return WeightableFactory.GetWeighted(Speeches);

    }

}





