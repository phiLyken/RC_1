using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_AlphaStackController : MonoBehaviour {

    AlphaStack Stack;

    public float AlphaPerSecond;

    public CanvasGroup CanvasGroup;

    public float GetAlpha()
    {
        return CanvasGroup.alpha;
    }

    public void Init(AlphaStack stack)
    {
        Stack = stack;
        Stack.OnUpdate += (f =>
        {
            StopAllCoroutines();
            StartCoroutine(UpdateAlphaTo(f));
        });

        CanvasGroup.alpha = stack.GetHighestAlpha();
      
    }


    IEnumerator UpdateAlphaTo(float update_to)
    {
        float t = 0;
        float start_alpha = CanvasGroup.alpha;

        float time_needed = Mathf.Abs(start_alpha - update_to) / AlphaPerSecond;
          

        while (t < 1)
        {
            CanvasGroup.alpha = Mathf.Lerp(start_alpha, update_to, t);

            t += Time.deltaTime / time_needed;
            yield return null;
        }

        CanvasGroup.alpha = update_to;

        if (Stack.StackUpdated != null)
            Stack.StackUpdated();
    }

}
