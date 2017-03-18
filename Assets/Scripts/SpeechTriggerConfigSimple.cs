using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;



public class SpeechTriggerConfigSimple : ScriptableObject
{
    public M_Math.R_Range Delay;

    public List<Speech> Speeches;

    public virtual Speech GetSpeech()
    {
      
            return M_Weightable.GetWeighted(Speeches);

    }

}





