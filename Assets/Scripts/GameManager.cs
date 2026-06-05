using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(JournalData))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [Header("References")]
    public JournalData journalData;
    public GameObject interactionPromptPrefab;

    [Header("Day/Night Cycle")]
    [SerializeField] List<Day> days;
    public int currentDay = 0;
    public bool isNight = false;
    public void AdvanceDay()
    {
        string nextScene;
        if (!isNight && days[currentDay].hasNight)
        {
            isNight = true;
            nextScene = days[currentDay].nightSceneName;
        }
        else
        {
            currentDay++;
            isNight = false;
            nextScene = days[currentDay].sceneName;
        }

        SceneManager.LoadScene(nextScene);
    }
}

[System.Serializable]
public class Day
{
    public string sceneName;
    public bool hasNight;
    public string nightSceneName;
}
