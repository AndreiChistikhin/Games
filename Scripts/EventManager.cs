using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    // path found event support
    static List<Traveler> pathFoundInvokers = new List<Traveler>();
    static List<UnityAction<float>> pathFoundListeners = new List<UnityAction<float>>();

    // path traversal complete event support
    static List<Traveler> pathTraversalCompleteInvokers = new List<Traveler>();
    static List<UnityAction> pathTraversalCompleteListeners = new List<UnityAction>();

    public static void AddPathFoundInvoker(Traveler invoker)
    {
        pathFoundInvokers.Add(invoker);
        foreach (UnityAction<float> listener in pathFoundListeners)
        {
            invoker.AddPathFoundListener(listener);
        }
    }

    public static void AddPathFoundListener(UnityAction<float> listener)
    {
        pathFoundListeners.Add(listener);
        foreach (Traveler invoker in pathFoundInvokers)
        {
            invoker.AddPathFoundListener(listener);
        }
    }

    public static void AddPathTraversalCompleteInvoker(Traveler invoker)
    {
        pathTraversalCompleteInvokers.Add(invoker);
        foreach (UnityAction listener in pathTraversalCompleteListeners)
        {
            invoker.AddPathTraversalCompleteListener(listener);
        }
    }

    public static void AddPathTraversalCompleteListener(UnityAction listener)
    {
        pathTraversalCompleteListeners.Add(listener);
        foreach (Traveler invoker in pathTraversalCompleteInvokers)
        {
            invoker.AddPathTraversalCompleteListener(listener);
        }
    }
}
