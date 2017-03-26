using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Example of using the StoppableCoroutine wrapper.
/// </summary>
public class Example : MonoBehaviour
{
    // MonoBehaviour.Start
    protected IEnumerator Start()
    {
       
        // Create the stoppable coroutine and store it
        var routine = MyCoroutine().MakeStoppable();
        // Pass the wrapper to StartCoroutine
        StartCoroutine(routine);

        var routine2 = RoutineWithParam("OMG", delegate
        { return !Input.GetKey(KeyCode.A); }).MakeStoppable(() => MDebug.Log("HAAHHA"));

        StartCoroutine(routine2);

        // Do stuff...
        yield return new WaitForSeconds(4);

        // Abort the coroutine by calling Stop() on the wrapper
        routine.Stop();
        routine2.Stop();
    }

    // Coroutine that runs indefinitely and can only
    // be stopped from the outside
    protected IEnumerator MyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            MDebug.Log("running...");
        }
    }

    protected IEnumerator RoutineWithParam(string foo, Func<bool> canExecute)
    {
        while (canExecute != null && canExecute() )
        {
            MDebug.Log("running too");
            yield return null;
        }

        MDebug.Log(foo);
    }
}