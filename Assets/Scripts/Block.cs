using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : Subject
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
        if (Input.GetKeyDown(KeyCode.RightArrow) && !_isPlaced)
        {
            blockDirection = BlockDirection.Right;
            NotifyObservers(BlockStates.Moving);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !_isPlaced)
        {
            blockDirection = BlockDirection.Left;
            NotifyObservers(BlockStates.Moving);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            fallDirection = FallDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            fallDirection = FallDirection.Down;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            timerMax -= 0.005f;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            timerMax = 2f;
        }
        if (!_isPlaced && _timer < timerMax)
        {
            _timer += Time.deltaTime;
        }
        else if (!_isPlaced && _timer >= timerMax)
        {
            _timer = 0f;
            switch (fallDirection)
            {
                case FallDirection.Down:
                    y--;
                    break;
                case FallDirection.Up:
                    y++;
                    break;
                default:
                    break;
            }
            NotifyObservers(BlockStates.Falling);
        }
        if (blockState == BlockStates.Placed)
        {
            NotifyObservers(blockState);
            blockState = BlockStates.Idle;
        }
    }

    public void SetPlaced(bool placed)
    {
        _isPlaced = placed;
    }
}
