using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class IntervalGenerator
{
    private const int SampleRate = 44100;
    private static string[] notes = new[] {"A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab"};
    
    public static void CreateClip(float freq, out AudioClip clip)
    {
        clip = AudioClip.Create("Tone", SampleRate, 1, SampleRate, false);
        var size = clip.frequency * (int) Mathf.Ceil(clip.length);

        float[] data = new float[size];
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * freq * count / SampleRate);
            count++;
        }

        clip.SetData(data, 0);
    }

    public static void ChooseInterval(out string interval, float freq1, out float freq2,
        string note1, out string note2)
    {
        string[] intervals = new[] {"Unison", "m2", "M2", "m3", "M3", "P4", "Tritone", "P5", "m6", "M6", "m7", "M7", "Octave"};
        var step = Random.Range(0, intervals.Length);
        interval = intervals[step];
        freq2 = freq1 * Mathf.Pow(1.06f,  step);
        note2 = GetNoteByStep(note1, step);
        Debug.Log(note2);
    }

    public static void ChooseInterval(out string interval, float freq1, out float freq2, string note1, out string note2,
        DifficultySetting difficulty)
    {
        string[] intervals = new[]
            {"Unison", "m2", "M2", "m3", "M3", "P4", "Tritone", "P5", "m6", "M6", "m7", "M7", "Octave"};
        var step = 0;
        if (difficulty == DifficultySetting.Easy)
        {
            step = Random.Range(1, 7);
        }
        else
        {
            step = Random.Range(0, intervals.Length);
        }

        interval = intervals[step];
        freq2 = freq1 * Mathf.Pow(1.06f, step);
        note2 = GetNoteByStep(note1, step);
        Debug.Log(note2);
    }

    private static string GetNoteByStep(string first, int step)
    {
        var index = Array.IndexOf(notes, first);
        var lastIndex = notes.Length - 1;
        if (index + step > lastIndex)
        {
            return notes[index + step - lastIndex - 1];
        }
        return notes[index + step];
    }
}
