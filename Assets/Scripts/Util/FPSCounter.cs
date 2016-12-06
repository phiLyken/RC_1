/* **************************************************************************
 * FPS COUNTER
 * **************************************************************************
 * Written by: Annop "Nargus" Prapasapong
 * Created: 7 June 2012
 * *************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* **************************************************************************
 * CLASS: FPS COUNTER
 * *************************************************************************/
[RequireComponent(typeof(GUIText))]
public class FPSCounter : MonoBehaviour
{
    /* Public Variables */
    public float frequency = 0.5f;

    /* **********************************************************************
	 * PROPERTIES
	 * *********************************************************************/
    public int FramesPerSec
    { get; protected set; }

    /* **********************************************************************
	 * EVENT HANDLERS
	 * *********************************************************************/
    /*
	 * EVENT: Start
	 */
    private void Start()
    {
        if(Application.isEditor || Debug.isDebugBuild)
            StartCoroutine(FPS());
    }

    /*
	 * EVENT: FPS
	 */
    private IEnumerator FPS()
    {
        for (;;)
        {
            for (int i = 0; i < 200; i++)
            {
               // Debug.Log("#fps# fps");
            }
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it
            FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            gameObject.GetComponent<Text>().text = FramesPerSec.ToString() + " fps";
        }
    }
}