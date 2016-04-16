using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float lifetime;
    public bool startOnEnable;

	void OnEnable (){
        if (startOnEnable)
            StartTimer();

    }	

    public void StartTimer()
    {
        StartCoroutine(DestroyAfter(lifetime));
    }
    IEnumerator DestroyAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
	
}
