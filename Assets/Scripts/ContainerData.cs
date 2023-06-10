using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container Data", menuName = "Container Data")]
public class ContainerData : ScriptableObject
{
    public Color color;
    public new String name;
}
