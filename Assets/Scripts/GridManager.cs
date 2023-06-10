using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Subject
{

    private Grid _grid;
    
    public Grid Grid => _grid;
    
    private Block _lastSpawned = null;

    private object nullCheck = null;

    private void Awake()
    {
        _grid = new Grid(6, 11, 1f, Vector3.down * 5.5f + Vector3.left * 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.CellOccupation += CellOccupied;
        EventManager.BlockDestroyed += RemoveObserver;
        EventManager.CleanRow += OnCleanRow;
        InputManager.fallDirectionChanged += ChangeFallDirection;
        InputManager.blockHorizontalMovement += ChangeHorizontalDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (ReferenceEquals(_lastSpawned, nullCheck))
        {
            return;
        }
        if (_lastSpawned.blockState == BlockStates.Falling)
        {
            int projectedY = _lastSpawned.fallDirection == Block.FallDirection.Down ? _lastSpawned.y - 1 : _lastSpawned.y + 1;
            if (!_grid.CheckOutOfBounds(projectedY) && !_grid.CheckGrid(_lastSpawned.x, _lastSpawned.y, _lastSpawned.fallDirection))
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
        _lastSpawned.x = 2;
        _lastSpawned.y = 5;
    }
    
    private void CellOccupied(int x, int y, bool occupied)
    {
        _grid.SetOccupied(x, y, occupied);
    }
    
    private void OnCleanRow(bool isBottom)
    {
        NotifyObservers(BlockStates.RowClean, isBottom);
    }

    private void ChangeFallDirection(Block.FallDirection direction)
    {
        _lastSpawned.fallDirection = direction;
    }
    
    private void ChangeHorizontalDirection(Block.BlockDirection direction)
    {
        // TODO: Add occupancy check
        var x = _lastSpawned.x;
        x += direction == Block.BlockDirection.Left ? -1 : 1;
        if (!_grid.CheckHorizontalBounds(x) && !_grid.GetCell(x, _lastSpawned.y).IsOccupied())
        {
            _lastSpawned.blockDirection = direction;
            return;
        }
        _lastSpawned.blockDirection = Block.BlockDirection.None;
    }
}
