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
    [SerializeField] private JournalData journalData;
    
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text descriptionLeft;
    [SerializeField] private TMP_Text descriptionRight;
    
    [SerializeField] private List<Button> dayButtons = new List<Button>();
    [SerializeField] private List<Button> nightButtons = new List<Button>();
    
    

    [Button]
    public void UpdateView()
    {
        UpdateTabs();

        // Day
        int dayIndex = dayButtons.IndexOf(selectedTab);
        if (dayIndex != -1)
        {
            title.text = "Day " + (dayIndex + 1);
            descriptionLeft.text = journalData.days[dayIndex];
            descriptionRight.text = journalData.days[dayIndex];
            return;
        }
        
        // Night
        int nightIndex = nightButtons.IndexOf(selectedTab);
        if (nightIndex != -1)
        {
            title.text = "Night " + (nightIndex + 1);
            descriptionLeft.text = journalData.nights[nightIndex];
            descriptionRight.text = journalData.nights[nightIndex];
            return;
        }
        
        // Fallback
        title.text = "Unspecified";
        descriptionLeft.text = "Unspecified";
        descriptionRight.text = "Unspecified";
    }
    
    public void UpdateTabs()
    {
        for (int i = 0; i < journalData.days.Length; i++)
        {
            if (journalData.days[i] != "" || journalData.days[i] == null)
                EnableCanvasGroup(dayButtons[i].GetComponent<CanvasGroup>());
            else
                DisableCanvasGroup(dayButtons[i].GetComponent<CanvasGroup>());
        }
        
        for (int i = 0; i < journalData.nights.Length; i++)
        {
            if (journalData.nights[i] != "" || journalData.nights[i] == null)
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

    private void OnEnable()
    {
        JournalData.JournalUpdated += UpdateView;
    }
    
    private void OnDisable()
    {
        JournalData.JournalUpdated -= UpdateView;
    }
}
