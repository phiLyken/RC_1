using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Wrapper around coroutines that allows to start them without using
/// their lexical name while still being able to stop them.
/// https://gist.github.com/sttz/4578868#file-stoppablecoroutine-cs
/// </summary>
/// 

public class StoppableCoroutine : IEnumerator
{
    // Wrapped generator method
    protected IEnumerator generator;

    Action callback;

    public StoppableCoroutine(IEnumerator generator, Action _callback)
    {
        this.callback = _callback;
        this.generator = generator;
    }

    public StoppableCoroutine(IEnumerator generator)
    {
        this.generator = generator;
    }

    // Stop the coroutine form being called again
    public void Stop()
    {
        generator = null;
        callback.AttemptCall();
        callback = null;
    }

    // IEnumerator.MoveNext
    public bool MoveNext()
    {
        if (generator != null)
        {
            bool hasNext = generator.MoveNext();
            if (!hasNext)
            {
                callback.AttemptCall();
                callback = null;
            }
            return hasNext;
        }
        else
        {
            callback.AttemptCall();
            callback = null;
            return false;
        }
    }

    // IEnumerator.Reset
    public void Reset()
    {
        if (generator != null)
        {
            generator.Reset();
            callback.AttemptCall();
            callback = null;
        }
    }

    // IEnumerator.Current
    public object Current
    {
        get
        {
            if (generator != null)
            {
                return generator.Current;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}

/// <summary>
/// Syntactic sugar to create stoppable coroutines.
/// </summary>
public static class StoppableCoroutineExtensions
{
    public static StoppableCoroutine MakeStoppable(this IEnumerator generator)
    {
        return new StoppableCoroutine(generator);
    }

    public static StoppableCoroutine MakeStoppable(this IEnumerator generator, Action callback)
    {
        return new StoppableCoroutine(generator, callback);
    }
}
 