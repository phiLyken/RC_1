using UnityEngine;
using System.Collections;

public class UI_EnemyTurn : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
        TurnSystem.Instance.OnStartTurn += CheckTurn;
	}
	
    void CheckTurn(ITurn turn)
    {
        if( (turn as Unit) != null && (turn as Unit).OwnerID == 1)
        {
          
                transform.GetChild(0).gameObject.SetActive(true);
          
        } else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnDestroy()
    {

    }
}
