using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalView : MonoBehaviour
{
    [Header("Data")]
    public Button selectedTab;

    
    [Header("References")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text descriptionLeft;
    [SerializeField] private TMP_Text descriptionRight;
    
    [SerializeField] private List<Button> dayButtons = new List<Button>();
    [SerializeField] private List<Button> nightButtons = new List<Button>();
    
    [Header("Settings")]
    [SerializeField] private List<string> dayStrings = new List<string>();
    [SerializeField] private List<string> nightStrings = new List<string>();
    
    

    [Button]
    public void UpdateView()
    {
        if (selectedTab == null)
            selectedTab = dayButtons[0];
        
        UpdateTabs();

        // Day
        int dayIndex = dayButtons.IndexOf(selectedTab);
        if (dayIndex != -1)
        {
            title.text = dayStrings[dayIndex];
            descriptionLeft.text = GameManager.instance.journalData.days[dayIndex];
            descriptionRight.text = GameManager.instance.journalData.days[dayIndex];
            return;
        }
        
        // Night
        int nightIndex = nightButtons.IndexOf(selectedTab);
        if (nightIndex != -1)
        {
            title.text = nightStrings[nightIndex];
            descriptionLeft.text = GameManager.instance.journalData.nights[nightIndex];
            descriptionRight.text = GameManager.instance.journalData.nights[nightIndex];
            return;
        }
        
        // Fallback
        title.text = "Unspecified";
        descriptionLeft.text = "Unspecified";
        descriptionRight.text = "Unspecified";
    }
    
    public void UpdateTabs()
    {
        for (int i = 0; i < GameManager.instance.journalData.days.Length; i++)
        {
            if (GameManager.instance.journalData.days[i] != "" || GameManager.instance.journalData.days[i] == null)
                EnableCanvasGroup(dayButtons[i].GetComponent<CanvasGroup>());
            else
                DisableCanvasGroup(dayButtons[i].GetComponent<CanvasGroup>());
        }
        
        for (int i = 0; i < GameManager.instance.journalData.nights.Length; i++)
        {
            if (GameManager.instance.journalData.nights[i] != "" || GameManager.instance.journalData.nights[i] == null)
                EnableCanvasGroup(nightButtons[i].GetComponent<CanvasGroup>());
            else
                DisableCanvasGroup(nightButtons[i].GetComponent<CanvasGroup>());
        }
    }

    public void SelectTab(Button button)
    {
        selectedTab = button;
        UpdateView();
    }

    void EnableCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    
    void DisableCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    private void Awake()
    {
        UpdateView();
    }

    // Event Bindings
    private void OnEnable()
    {
        GameManager.instance.journalData.JournalUpdated += UpdateView;
    }
    
    private void OnDisable()
    {
        GameManager.instance.journalData.JournalUpdated -= UpdateView;
    }
}
