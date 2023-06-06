using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action<int, int, bool> CellOccupation; //bool indicates occupied, int x, int y
    public static event Action<int, int, Block> BlockPlaced; // position, block
    public static event Action<Block> BlockDestroyed; // Destroyed block
    public static event Action<bool> CleanRow; //bool indicates bottom
    public static event Action<BlockStates, bool> NotifyAllObservers; //Event to notify all observers so that we can cleeeeaaaan the row ohh yeaahhh
    // wah wah wahh wa wah wah wahhh
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void SetCellOccupation(int x, int y, bool occupied)
    {
        CellOccupation?.Invoke(x, y, occupied);
    }
    
    public static void CallBlockPlaced(int x, int y, Block block)
    {
        BlockPlaced?.Invoke(x, y, block);
    }
    
    public static void OnBlockDestroyed(Block block)
    {
        BlockDestroyed?.Invoke(block);
    }
    
    public static void OnCleanRow(bool bottom)
    {
        CleanRow?.Invoke(bottom);
    }

    public static void NotifyAll(BlockStates state, bool bottom)
    {
        NotifyAllObservers?.Invoke(state, bottom);
    }
}
