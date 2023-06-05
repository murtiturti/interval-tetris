using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] private Container[] topContainers = new Container[6];
    [SerializeField] private Container[] bottomContainers = new Container[6];
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpContainers();
    }

    public Container GetContainer(bool bottom, int index)
    {
        if (bottom)
        {
            return bottomContainers[index];
        }

        return topContainers[index];
    }

    public void CleanRow(bool bottom)
    {
        if (bottom)
        {
            foreach (var cont in bottomContainers)
            {
                cont.RemoveFirstAdded();
            }
        }
        else
        {
            foreach (var cont in topContainers)
            {
                cont.RemoveFirstAdded();
            }
        }
    }

    private void SetUpContainers()
    {
        foreach (var cont in bottomContainers)
        {
            cont.bottom = true;
        }

        foreach (var cont in topContainers)
        {
            cont.bottom = false;
        }
    }
}
