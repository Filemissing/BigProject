using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(JournalData))]
[RequireComponent(typeof(InventoryData))]
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
    public InventoryData inventoryData;
    public GameObject interactionPromptPrefab;
    public InputHandler inputHandler;
    public CurrentMainUIManager currentMainUIManager;
    public Settings settings;

    [Header("Day/Night Cycle")]
    [SerializeField] List<Day> days;
    public int currentDay = 0;
    public bool isNight = false;

    private void Start()
    {
        // see if we started at a different day - makes it easier to test later days
        Scene currentScene = SceneManager.GetActiveScene();
        for (int i = 0; i < days.Count; i++)
        {
            Day day = days[i];
            if (day.sceneName == currentScene.name)
            {
                currentDay = i;
                break;
            }
            else if (day.hasNight && day.nightSceneName == currentScene.name)
            {
                currentDay = i;
                isNight = true;
                break;
            }
        }
    }
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
