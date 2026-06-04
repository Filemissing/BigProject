using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalView : MonoBehaviour
{
    [Header("Data")]
    public List<string> days = new List<string>();
    public List<string> nights = new List<string>();
    
    [SerializeField] public Button selectedTab;

    
    [Header("References")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text descriptionLeft;
    [SerializeField] private TMP_Text descriptionRight;
    
    [SerializeField] private List<Button> dayButtons = new List<Button>();
    [SerializeField] private List<Button> nightButtons = new List<Button>();
    
    

    [Button]
    public void UpdateView()
    {
        title.text = "Day 1";
        descriptionLeft.text = days[0];
        descriptionRight.text = "nothing rn";
    }
    
    [Button]
    public void UpdateTabs()
    {
        for (int i = 0; i < days.Count; i++)
        {
            if (days[i] != "")
                EnableCanvasGroup(dayButtons[i].GetComponent<CanvasGroup>());
            else
                DisableCanvasGroup(dayButtons[i].GetComponent<CanvasGroup>());
        }
        
        for (int i = 0; i < nights.Count; i++)
        {
            if (nights[i] != "")
                EnableCanvasGroup(nightButtons[i].GetComponent<CanvasGroup>());
            else
                DisableCanvasGroup(nightButtons[i].GetComponent<CanvasGroup>());
        }
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
}
