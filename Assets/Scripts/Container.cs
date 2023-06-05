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
            // TODO: unoccupy cell
            stack.RemoveAt(0);
            Destroy(temp.gameObject);
        }
        else
        {
            var temp = stack[0];
            stack.RemoveAt(0);
            Destroy(temp.gameObject);
            // TODO: make fall
        }
    }
}
