using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 


public class ObjectiveConfig : ScriptableObject {

    public string Title;
    public string Description;

    public string ID;

    public GameObject UI_Info;

    public ObjectiveCondition Condition;

    public List<ObjectiveSetup> Setup;
  


}
