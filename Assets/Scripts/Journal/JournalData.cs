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
    
    
    
    // Use to add a Note to the current day
    public void AddNoteToCurrentDay(string note)
    {
        if (GameManager.instance.isNight == false)
            days[GameManager.instance.currentDay] += note + "<br><br>";
        else
            nights[GameManager.instance.currentDay] += note + "<br><br>";
        
        JournalUpdated?.Invoke();
    }
}
