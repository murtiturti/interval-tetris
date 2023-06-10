using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : ScriptableObject
{
    private float _frequency;
    public new string name;
    public Color color;
    public string targetNoteName;
    public Dictionary<string, Color> Colors = new Dictionary<string, Color>();

    public BlockData(float frequency, string name, string targetNoteName)
    {
        Color[] colors =
        {
            new Color(1f, 0.50049f, 0f), new Color(0.8867924f, 0.2969918f, 0.3032145f), // A, Bb
            new Color(0.9245283f, 0.8183578f, 0.248576f), new Color(0.8605546f, 0.9254902f, 0.2470588f), // B, C
            new Color32(0x99, 0xEC,0x34, 0xFF), new Color32(0x4E, 0xE9, 0x3F, 0xFF), // Db, D
            new Color32(0x34, 0xEC, 0x9C, 0xFF), new Color32(0x32, 0xE9, 0x65, 0xFF), // Eb, E
            new Color32(0x38, 0xE7, 0x7F, 0xFF), new Color32(0x2F, 0xE7, 0xB8, 0xFF), // F, Gb
            new Color32(0x3A, 0xE0, 0xF3, 0xFF), new Color32(0x29, 0x9A, 0xF5, 0xFF), // G, Ab
        };
        string[] names = {"A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab"};

        for (int i = 0; i < names.Length; i++)
        {
            Colors.Add(names[i], colors[i]);
        }

        _frequency = frequency;
        this.name = name;
        this.targetNoteName = targetNoteName;
        this.color = Colors[this.name];
    }
}
