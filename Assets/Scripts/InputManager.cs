using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnVerticalInput(Block.FallDirection dir)
    {
        fallDirectionChanged?.Invoke(dir);
    }
    
    private void OnHorizontalInput(Block.BlockDirection dir)
    {
        blockHorizontalMovement?.Invoke(dir);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnVerticalInput(Block.FallDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnVerticalInput(Block.FallDirection.Down);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnHorizontalInput(Block.BlockDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnHorizontalInput(Block.BlockDirection.Left);
        }
    }
}
