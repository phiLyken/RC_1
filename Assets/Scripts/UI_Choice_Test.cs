using UnityEngine;
using System.Collections;

public class UI_Choice_Test : MonoBehaviour {


	void Start () {

        UI_Choice.CreateUIChoice(new string[]
        {
            "+1 Damage", "+1 Move Range","+1 Rest"
        }, Choice);
 
    }
	
    void Choice(int i)
    {
        Debug.Log("Loot choice test :"+i);
    }


}
