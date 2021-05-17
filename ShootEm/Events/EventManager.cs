using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static List<Ship> healthChangedInvokers = new List<Ship>();
    static List<UnityAction<int>> healthChangedListeners =
        new List<UnityAction<int>>();

    static List<Ship> gameOverInvokers = new List<Ship>();
    static List<UnityAction> gameOverListeners =
        new List<UnityAction>();

    public static void AddHealthChangedInvoker(Ship invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (UnityAction<int> listener in healthChangedListeners)
        {
            invoker.AddHealthChangedListener(listener);
        }
        healthChangedInvokers.Add(invoker);
    }

    public static void AddHealthChangedListener(UnityAction<int> listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (Ship invoker in healthChangedInvokers)
        {
            invoker.AddHealthChangedListener(listener);
        }
        healthChangedListeners.Add(listener);
    }

    public static void AddGameOverInvoker(Ship invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (UnityAction listener in gameOverListeners)
        {
            invoker.AddGameOverListener(listener);
        }
        gameOverInvokers.Add(invoker);
    }

    public static void AddGameOverListener(UnityAction listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (Ship invoker in gameOverInvokers)
        {
            invoker.AddGameOverListener(listener);
        }
        gameOverListeners.Add(listener);
    }
}
