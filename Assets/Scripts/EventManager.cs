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
    public static event Action<bool> MakeFall; //bool indicates bottom 
    public static event Action ReadyForSpawn; //Event to notify that the grid is ready for a new block to spawn
    public static event Action GameOver; //Event to notify that the game is over
    public static event Action<int> ScoreChanged; //Event to notify that the score has changed
    public static event Action<int, UIManager.UpdateType> UpdateUI; //Event to notify that the UI needs to be updated
    public static event Action<string, bool> UpdateInterval; //Event to notify that the interval needs to be updated
    public static event Action<bool> FlashAnswer; //Event to notify that the answer needs to be flashed
    public static event Action<bool> GamePaused; //Subscribed in GridManager
    public static event Action NextTutorial; 


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

    public static void InvokeMakeFall(bool bottom)
    {
        MakeFall?.Invoke(bottom);
    }
    
    public static void OnReadyForSpawn()
    {
        ReadyForSpawn?.Invoke();
    }
    
    public static void OnGameOver()
    {
        GameOver?.Invoke();
    }

    public static void OnScoreChange(int score)
    {
        ScoreChanged?.Invoke(score);
    }

    public static void OnUpdateUI(int score, UIManager.UpdateType type)
    {
        UpdateUI?.Invoke(score, type);
    }
    
    public static void UpdateIntervalUI(string interval, bool ascending)
    {
        UpdateInterval?.Invoke(interval, ascending);
    }
    
    public static void InvokeFlash(bool flash)
    {
        FlashAnswer?.Invoke(flash);
    }
    
    public static void OnGamePaused(bool isPaused)
    {
        GamePaused?.Invoke(isPaused);
    }
    
    public static void OnNextTutorial()
    {
        NextTutorial?.Invoke();
    }
}
