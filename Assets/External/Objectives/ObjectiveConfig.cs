using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
using UnityEditor;

public class ObjectiveConfig : ScriptableObject {

    public string Title;
    public string Description;

    public GameObject UI_Info;

    public ObjectiveCondition Condition;

    public ObjectiveSetup Setup;
  


}
