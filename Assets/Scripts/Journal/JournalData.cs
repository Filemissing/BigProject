using System;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class JournalData : MonoBehaviour
{
    [Header("Data")]
    public string[] days = new string[5];
    public string[] nights = new string[5];

    public static event Action JournalUpdated;
    
    public void AddNoteToCurrentDay(string note)
    {
        if (GameManager.instance.isNight == true)
            days[GameManager.instance.currentDay] += note;
        else
            nights[GameManager.instance.currentDay] += note;
        
        JournalUpdated?.Invoke();
    }
}
