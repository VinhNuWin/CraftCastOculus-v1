using System;
using System.Collections;
using System.Collections.Generic;  // Make sure to include this for using generic Queue<T>
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    private TextLog textLog;
    private static readonly Queue<Action> executeOnMainThreadQueue = new Queue<Action>();
    private static MainThreadDispatcher instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // TextLog.Instance.Log("Update running");
        while (executeOnMainThreadQueue.Count > 0)
        {
            executeOnMainThreadQueue.Dequeue().Invoke();
        }
    }

    public static void Enqueue(Action action)
    {
        if (action == null) return;

        lock (executeOnMainThreadQueue)
        {
            executeOnMainThreadQueue.Enqueue(action);
        }
    }
}
