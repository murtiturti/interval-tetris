using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour, IObserver
{
    public int x, y;
    public BlockData blockData;

    private float _timer = 0f;
    [SerializeField, Range(1f, 5f)]
    private float timerMax = 2f;

    private bool _isPlaced = false;

    public enum BlockDirection
    {
        None, Left, Right
    }
    
    public enum FallDirection
    {
        Down, Up
    }

    public BlockDirection blockDirection;
    public FallDirection fallDirection;

    public BlockStates blockState;
    
    // Start is called before the first frame update
    void Start()
    {
        blockDirection = BlockDirection.None;
        fallDirection = FallDirection.Down;
        blockState = BlockStates.Falling;
        GetComponent<SpriteRenderer>().color = blockData.color;
        GetComponentInChildren<TextMeshProUGUI>().text = blockData.name;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        _timer += Time.deltaTime;
        if (_timer >= timerMax)
        {
            if (blockState == BlockStates.Falling)
            {
                if (fallDirection == FallDirection.Down)
                {
                    Move(Vector3.down);
                }
                else if (fallDirection == FallDirection.Up)
                {
                    Move(Vector3.up);
                }
            }
            else if (blockState == BlockStates.Idle)
            {
                blockState = BlockStates.Placed;
                EventManager.CallBlockPlaced(x, y, this);
            }
            _timer = 0f;
        }
        if (blockDirection == BlockDirection.Left)
        {
            Move(Vector3.left);
            blockDirection = BlockDirection.None;
        }
        else if (blockDirection == BlockDirection.Right)
        {
            Move(Vector3.right);
            blockDirection = BlockDirection.None;
        }
        */
    }

    public void SetPlaced(bool placed)
    {
        _isPlaced = placed;
    }

    public void OnNotify(BlockStates state)
    {
        if (state == BlockStates.Falling)
        {
            if (fallDirection == FallDirection.Down)
            {
                Move(Vector3.down);
            }
            else if (fallDirection == FallDirection.Up)
            {
                Move(Vector3.up);
            }
        }
        else if (state == BlockStates.Idle)
        {
            blockState = BlockStates.Placed;
            EventManager.CallBlockPlaced(x, y, this);
        }
    }

    public void OnNotify(BlockStates state, bool bottom)
    {
        if (state == BlockStates.RowClean)
        {
            if (bottom)
            {
                if (y < 5)
                {
                    Move(Vector3.down);
                }
            }
            else
            {
                if (y > 5)
                {
                    Move(Vector3.up);
                }
            }
        }
    }

    public void OnHorizontalMovement(BlockDirection direction)
    {
        if (direction == BlockDirection.Left)
        {
            Move(Vector3.left);
            blockDirection = BlockDirection.None;
        }
        else if (direction == BlockDirection.Right)
        {
            Move(Vector3.right);
            blockDirection = BlockDirection.None;
        }
    }

    public void OnVerticalInput(FallDirection direction)
    {
        fallDirection = direction;
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction.normalized;
        EventManager.SetCellOccupation(x, y, false);
        y += (int) direction.y;
        x += (int) direction.x;
        EventManager.SetCellOccupation(x, y, true);
    }
}
