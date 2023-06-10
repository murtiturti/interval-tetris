using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public ContainerData containerData;
    public bool bottom;

    [SerializeField] private List<Block> stack = new List<Block>();
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = containerData.color;
        GetComponentInChildren<TextMeshProUGUI>().text = containerData.name;
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
            // TODO: make fall
        }
    }

    public void OnCorrectPlacement()
    {
        if (stack.Count >= 2)
        {
            // Clear stack
            foreach (var block in stack)
            {
                EventManager.SetCellOccupation(block.x, block.y, false);
                // TODO: Unsubscribe from block from GridManager
                EventManager.OnBlockDestroyed(block);
                stack.Remove(block);
                Destroy(block.gameObject);
            }

            return;
        }
        // If the placed block is the first block on the container, clean the row
        EventManager.OnCleanRow(bottom);
    }

    public bool NeedsRowClean()
    {
        if (stack.Count >= 2)
        {
            return true;
        }

        return false;
    }
}
