using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(BlockStates state)
    {
        _observers.ToList().ForEach((_observer) =>
        {
            _observer.OnNotify(state);
        });
    }

    protected void NotifyObservers(BlockStates state, bool bottom)
    {
        _observers.ToList().ForEach((_observer) =>
        {
            _observer.OnNotify(state, bottom);
        });
    }
 }
