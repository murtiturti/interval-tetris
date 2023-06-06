using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void OnNotify(BlockStates state);

    void OnNotify(BlockStates state, bool bottom);
}
