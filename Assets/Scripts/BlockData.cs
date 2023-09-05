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
            new Color32(0xFD, 0xC5, 0x23, 0xFF), new Color32(0x94, 0x52, 0xA7, 0xFF), // A, Bb
            new Color32(0x9D, 0xBA, 0x43, 0xFF), new Color32(0xE1, 0x2F, 0x5B, 0xFF), // B, C
            new Color32(0x04, 0x9F,0x8F, 0xFF), new Color32(0xF7, 0x94, 0x35, 0xFF), // Db, D
            new Color32(0x4D, 0x76, 0xBA, 0xFF), new Color32(0xF0, 0xDD, 0x1C, 0xFF), // Eb, E
            new Color32(0xB6, 0x32, 0x9D, 0xFF), new Color32(0x43, 0xA1, 0x51, 0xFF), // F, Gb
            new Color32(0xF2, 0x62, 0x32, 0xFF), new Color32(0x00, 0xA1, 0xD9, 0xFF), // G, Ab
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
