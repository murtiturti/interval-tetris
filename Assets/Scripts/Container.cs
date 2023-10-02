using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public ContainerData containerData;
    public bool bottom;

    [SerializeField] private List<Block> stack;
    private Sprite _contSprite;

    private void Awake()
    {
        _contSprite = Resources.Load<Sprite>("Sprites/lightFactoryPanel");
        ScaleSprite();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = containerData.color;
        GetComponentInChildren<TextMeshProUGUI>().text = containerData.name;
        stack = new List<Block>();
    }

    public bool CheckPlacement(Block block)
    {
        return containerData.name == block.blockData.targetNoteName;
    }

    public void AddToStack(Block block)
    {
        stack.Add(block);
    }

    public void RemoveFromStack(Block block)
    {
        stack.Remove(block);
    }
    
    public void RemoveFirstAdded()
    {
        if (stack.Count == 0)
        {
            return;
        }

        if (stack.Count == 1)
        {
            var temp = stack[0];
            stack.RemoveAt(0);
            EventManager.SetCellOccupation(temp.x, temp.y, false);
            EventManager.OnBlockDestroyed(temp);
            Destroy(temp.gameObject);
        }
        else
        {
            var temp = stack[0];
            stack.RemoveAt(0);
            EventManager.SetCellOccupation(temp.x, temp.y, false);
            EventManager.OnBlockDestroyed(temp);
            Destroy(temp.gameObject);
        }
    }

    public void OnCorrectPlacement()
    {
        if (stack.Count >= 2)
        {
            // Clear stack
            /*
            foreach (var block in stack.ToArray())
            {
                EventManager.SetCellOccupation(block.x, block.y, false);
                // TODO: Unsubscribe block from GridManager
                EventManager.OnBlockDestroyed(block);
                stack.Remove(block);
                Destroy(block.gameObject);
            }
            */
            var dir = bottom ? DirectionConstants.down : DirectionConstants.up;
            StartCoroutine(MakeWholeStackFall(dir));
            EventManager.OnScoreChange(50);
            return;
        }
        // If the placed block is the first block on the container, clean the row
        EventManager.OnCleanRow(bottom);
        EventManager.OnScoreChange(100);
    }

    public bool IsStackFull()
    {
        return stack.Count > 5;
    }

    private IEnumerator MakeWholeStackFall(Vector3 direction)
    {
        // Makes the whole stack fall out of the grid
        while (stack.Count != 0)
        {
            var temp = stack[0];
            EventManager.SetCellOccupation(temp.x, temp.y, false);
            EventManager.OnBlockDestroyed(temp);
            stack.Remove(temp);
            Destroy(temp.gameObject);
            if (stack.Count == 0)
            {
                yield return null;
            }
            yield return StartCoroutine(FallOnce(direction));
        }
    }

    public IEnumerator FallOnce(Vector3 direction)
    {
        // Makes every block in the stack fall one cell
        // Does NOT handle destroying blocks
        var timeMax = 0.2f;
        var time = 0f;
        var remStackSize = stack.Count;
        for (int i = 0; i < remStackSize; i++)
        {
            
            while (time < timeMax)
            {
                time += Time.deltaTime;
                yield return null;
            }
            stack[i].Move(direction);
            time = 0f;
            timeMax /= 2f;
        }
    }

    public bool NeedsRowClean()
    {
        if (stack.Count >= 2)
        {
            return true;
        }

        return false;
    }

    private void ScaleSprite()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _contSprite;
        var sprite = spriteRenderer.sprite;
        Vector2 spriteSize = sprite.bounds.size;
        var cellSize = 12f / 11f;
        var scaleX = cellSize / spriteSize.x;
        var scaleY = cellSize / spriteSize.y;
        
        spriteRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}
