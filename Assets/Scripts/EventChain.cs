using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain<T>
{
    public List<Action<T>> chain = new List<Action<T>>();
    
    public void AddEvent(Action<T> action)
    {
        chain.Add(action);
    }
    
    public void RemoveEvent(Action<T> action)
    {
        chain.Remove(action);
    }
    
    public void ExecuteChain(T value)
    {
        foreach (var action in chain)
        {
            action.Invoke(value);
        }
    }

    public void SubscribeToAction(Action<T> action)
    {
        
    }
}
