using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Subject
{

    private Grid _grid;
    [SerializeField] private Spawner spawner;
    private Block _lastSpawned;
    
    [SerializeField] private ContainerManager containerManager;

    // Start is called before the first frame update
    void Start()
    {
        _grid = new Grid(6, 11, 1f, Vector3.down * 5f + Vector3.left * 3f);
        EventManager.CellOccupation += CellOccupied;
        EventManager.BlockDestroyed += RemoveObserver;
        EventManager.CleanRow += OnCleanRow;
        EventManager.NotifyAllObservers += NotifyObservers;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastSpawned.blockState == BlockStates.Falling)
        {
            int projectedY = _lastSpawned.fallDirection == Block.FallDirection.Down ? _lastSpawned.y - 1 : _lastSpawned.y + 1;
            if (!_grid.CheckGrid(_lastSpawned.x, _lastSpawned.y, _lastSpawned.fallDirection) && !_grid.CheckOutOfBounds(projectedY))
            {
                _lastSpawned.OnNotify(BlockStates.Falling);
            }
            else
            {
                _lastSpawned.OnNotify(BlockStates.Idle);
            }
        }
    }
    
    public void SetLastSpawned(Block block)
    {
        _lastSpawned = block;
        AddObserver(block);
    }
    
    private void CellOccupied(int x, int y, bool occupied)
    {
        _grid.SetOccupied(x, y, occupied);
    }
    
    private void OnCleanRow(bool isBottom)
    {
        NotifyObservers(BlockStates.RowClean, isBottom);
    }
}
