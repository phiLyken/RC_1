using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_TurnListTest : MonoBehaviour {

    public TurnSystemMockData MockData;
    public UI_TurnList TurnList;
    List<ITurn> turnables;


    void Start()
    {
        turnables = MockData.GetMockTurnList();
        TurnList.Init(null);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            turnables.Shuffle();
            TurnList.OnListUpdate(turnables);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            int randomIndex2 = Random.Range(0, turnables.Count);
            int randomIndex1 = Random.Range(0, turnables.Count);

            ITurn swapped1 = turnables[randomIndex1];
            ITurn swapped2 = turnables[randomIndex2];

            turnables[randomIndex1] = swapped2;
            turnables[randomIndex2] = swapped1;

            TurnList.OnListUpdate(turnables);
        }

        if (Input.GetButtonDown("Jump"))
        {
            ITurn first = turnables[0];
            turnables.RemoveAt(0);
            turnables.Add(first);

            TurnList.OnListUpdate(turnables);
        }
    }
}
