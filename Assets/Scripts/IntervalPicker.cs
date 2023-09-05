using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public static class IntervalPicker
{
    // Start is called before the first frame update
    private static string[] _notes = {"C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B"};
    private static int[] _octaves = {2, 3};
    private static string[] _intervals = 
        {"Unison", "m2", "M2", "m3", "M3", "P4", "Tritone", "P5", "m6", "M6", "m7", "M7", "Octave"};

    public static void PickNotes(out string note1, out string note2, 
        out string interval, bool ascending, DifficultySetting difficulty)
    {
        var note1Octave = ascending ? 
            _octaves[Random.Range(0, _octaves.Length - 1)] : _octaves[Random.Range(1, _octaves.Length)];
        note1 = _notes[Random.Range(0, _notes.Length)];
        var step = difficulty == DifficultySetting.Easy ? Random.Range(1, 7) : Random.Range(0, _intervals.Length);
        interval = _intervals[step];
        var note2Octave = note1Octave;
        note2 = GetNoteByStep(note1, step, ascending, out var octaveChange);
        if (octaveChange && !ascending)
        {
            // If an octave change is required and the interval is descending, the octave of the second note must be lower
            note2Octave = note1Octave - 1;
        }
        else if (octaveChange && ascending)
        {
            // If an octave change is required and the interval is ascending, the octave of the second note must be higher
            note2Octave = note1Octave + 1;
        }

        note1 += note1Octave;
        note2 += note2Octave;
    }
    
    
    private static string GetNoteByStep(string first, int step, bool ascending, out bool octaveChange)
    {
        var index = Array.IndexOf(_notes, first);
        octaveChange = false;
        if (!ascending)
        {
            if (index - step < 0)
            {
                octaveChange = true;
                return _notes[_notes.Length + index - step];
            }
            return _notes[index - step];
        }
        var lastIndex = _notes.Length - 1;
        if (index + step > lastIndex)
        {
            octaveChange = true;
            return _notes[index + step - lastIndex - 1];
        }
        return _notes[index + step];
    }
}
