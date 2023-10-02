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
                Move(DirectionConstants.down);
            }
            else if (fallDirection == FallDirection.Up)
            {
                Move(DirectionConstants.up);
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
                    Move(DirectionConstants.down);
                }
            }
            else
            {
                if (y > 5)
                {
                    Move(DirectionConstants.up);
                }
            }
        }
    }

    public void OnHorizontalMovement(BlockDirection direction)
    {
        if (direction == BlockDirection.Left)
        {
            Move(DirectionConstants.left);
            blockDirection = BlockDirection.None;
        }
        else if (direction == BlockDirection.Right)
        {
            Move(DirectionConstants.right);
            blockDirection = BlockDirection.None;
        }
    }

    public void OnVerticalInput(FallDirection direction)
    {
        fallDirection = direction;
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction;
        EventManager.SetCellOccupation(x, y, false);
        y += (int) direction.y;
        x += (int) direction.x;
        EventManager.SetCellOccupation(x, y, true);
    }
}
