using UnityEngine;
using System.Collections;

public class TileCrumbleTest : MonoBehaviour {

    public int crumbleTurns;
    int row = 3;
	// Use this for initialization
	void Start () {

      for(int i = 0; i < crumbleTurns; i++)
        {
            GetComponent<Tile>().OnCrumbleTurn(i);
        }
	}

}
