using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : Subject
{

    private Grid _grid;
    
    public Grid Grid => _grid;
    
    private Block _lastSpawned = null;

    private object nullCheck = null;
    private float _timer;
    [SerializeField, Range(0.5f, 5f)]
    private float timerMax = 2f;

    private float _intervalTimer = 0f;
    
    private bool _gameOver = false;

    private void Awake()
    {
        _grid = new Grid(6, 11, 12f/11f, Vector3.down * 7f + Vector3.left * 3.25f);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.CellOccupation += CellOccupied;
        EventManager.BlockDestroyed += RemoveObserver;
        //EventManager.CleanRow += OnCleanRow;
        EventManager.MakeFall += OnCleanRow;
        InputManager.fallDirectionChanged += ChangeFallDirection;
        InputManager.blockHorizontalMovement += ChangeHorizontalDirection;
        InputManager.SpeedUp += OnSpeedUp;
        InputManager.ResetSpeed += OnResetSpeed;
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ReferenceEquals(_lastSpawned, nullCheck))
        {
            return;
        }

        if (_gameOver)
        {
            return;
        }
        _timer += Time.deltaTime;
        _intervalTimer += Time.deltaTime;
        if (_timer >= timerMax)
        {
            _timer = 0f;
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

        if (_intervalTimer >= 2.5f)
        {
            IntervalPlayer.Instance.PlayInterval(true);
            _intervalTimer = 0f;
        }
    }
    
    public void SetLastSpawned(Block block)
    {
        _lastSpawned = block;
        AddObserver(block);
        _lastSpawned.x = 2;
        _lastSpawned.y = 5;
        _intervalTimer = 0f;
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
        _lastSpawned.OnVerticalInput(direction);
    }
    
    private void ChangeHorizontalDirection(Block.BlockDirection direction)
    {
        // TODO: Add occupancy check
        var x = _lastSpawned.x;
        x += direction == Block.BlockDirection.Left ? -1 : 1;
        if (!_grid.CheckHorizontalBounds(x) && !_grid.GetCell(x, _lastSpawned.y).IsOccupied())
        {
            _lastSpawned.OnHorizontalMovement(direction);
            return;
        }
        _lastSpawned.blockDirection = Block.BlockDirection.None;
    }
    
    public void GameOver()
    {
        _gameOver = true;
    }

    private void OnSpeedUp()
    {
        if (timerMax - 0.5f >= 0.5f)
        {
            timerMax -= 0.5f;
        }
    }

    private void OnResetSpeed()
    {
        timerMax = 2f;
        _timer = 0f;
    }
}
