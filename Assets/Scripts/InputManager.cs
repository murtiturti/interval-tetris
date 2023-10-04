using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class InputManager : MonoBehaviour
{
    
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(InputManager));
                    _instance = singletonObject.AddComponent<InputManager>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    #region Events

    public static event Action<Block.FallDirection> fallDirectionChanged;
    public static event Action<Block.BlockDirection> blockHorizontalMovement;
    public static event Action SpeedUp;
    public static event Action ResetSpeed;

    #endregion

    #region DoubleClickVars

    private bool _firstClick = false;
    private bool _doubleClick = false;
    private float _holdTimer = 0f;
    [SerializeField] private float holdMax = 0.5f;
    private float _timeOutTimer = 0f;
    [SerializeField] private float timeOutMax = 0.5f;
    private bool _holding = false;

    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void OnSwipeHandler(string id)
    {
        switch (id)
        {
            case DirectionId.ID_UP:
                OnVerticalInput(Block.FallDirection.Up);
                break;
            case DirectionId.ID_DOWN:
                OnVerticalInput(Block.FallDirection.Down);
                break;
            case DirectionId.ID_LEFT:
                OnHorizontalInput(Block.BlockDirection.Left);
                break;
            case DirectionId.ID_RIGHT:
                OnHorizontalInput(Block.BlockDirection.Right);
                break;
            default:
                break;
        }
    }

    private void OnVerticalInput(Block.FallDirection dir)
    {
        fallDirectionChanged?.Invoke(dir);
        EventManager.OnNextTutorial();
    }
    
    private void OnHorizontalInput(Block.BlockDirection dir)
    {
        blockHorizontalMovement?.Invoke(dir);
        EventManager.OnNextTutorial();
    }
    
    private void InvokeSpeedUp()
    {
        SpeedUp?.Invoke();
        EventManager.OnNextTutorial();
    }
    
    private void InvokeResetSpeed()
    {
        ResetSpeed?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (_firstClick)
        {
            _timeOutTimer += Time.deltaTime;
        }

        if (_firstClick && _timeOutTimer >= timeOutMax && !_doubleClick)
        {
            _firstClick = false;
            _timeOutTimer = 0f;
        }

        if (Input.GetMouseButtonDown(0) && _firstClick)
        {
            // second click
            _doubleClick = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _firstClick = true;
        }

        if (Input.GetMouseButton(0) && _doubleClick)
        {
            _holdTimer += Time.deltaTime;
            _holding = true;
            if (_holdTimer >= holdMax)
            {
                // raise event
                InvokeSpeedUp();
                _holdTimer = 0f;
            }
        }

        if (Input.GetMouseButtonUp(0) && _holding)
        {
            _firstClick = false;
            _doubleClick = false;
            _holdTimer = 0f;
            _timeOutTimer = 0f;
            _holding = false;
            // raise event
            InvokeResetSpeed();
        }
    }
}
