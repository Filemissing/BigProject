using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup sidePanelCanvasGroup;
    [SerializeField] private RectTransform sidePanel;
    [SerializeField] private CanvasGroup darkEffectCanvasGroup;

    [Header("Settings")]
    [SerializeField] private float duration = .3f;
    [SerializeField] private Vector2 panelEffectOffset = new  Vector2(-200, 0);

    bool toggle = false;
    Vector2 defaultPanelPosition;
    
    
    
    // Functions
    private void Awake()
    {
        defaultPanelPosition = sidePanel.anchoredPosition;
        ForceInvisible();
    }
    
    
    
    // Visiblity
    public void ToggleVisibility()
    {
        toggle = !toggle;
        
        if (toggle)
            SetVisible();
        else
            SetInvisible();
    }
    
    private void SetVisible()
    {
        darkEffectCanvasGroup.blocksRaycasts = true;
        darkEffectCanvasGroup.interactable = true;
        darkEffectCanvasGroup.DOFade(1, duration).SetEase(Ease.OutCubic);
        
        sidePanelCanvasGroup.blocksRaycasts = true;
        sidePanelCanvasGroup.interactable = true;
        sidePanelCanvasGroup.DOFade(1, duration).SetEase(Ease.OutCubic);
        
        sidePanel.anchoredPosition = defaultPanelPosition + panelEffectOffset;
        sidePanel.DOAnchorPos(defaultPanelPosition, duration).SetEase(Ease.OutCubic);
    }
    
    private void SetInvisible()
    {
        darkEffectCanvasGroup.blocksRaycasts = false;
        darkEffectCanvasGroup.interactable = false;
        darkEffectCanvasGroup.DOFade(0, duration).SetEase(Ease.OutCubic);
        
        sidePanelCanvasGroup.blocksRaycasts = false;
        sidePanelCanvasGroup.interactable = false;
        sidePanelCanvasGroup.DOFade(0, duration).SetEase(Ease.OutCubic);
        
        sidePanel.anchoredPosition = defaultPanelPosition;
        sidePanel.DOAnchorPos(defaultPanelPosition + panelEffectOffset, duration).SetEase(Ease.OutCubic);
    }
    
    private void ForceInvisible()
    {
        darkEffectCanvasGroup.blocksRaycasts = false;
        darkEffectCanvasGroup.interactable = false;
        darkEffectCanvasGroup.alpha = 0;
        
        sidePanelCanvasGroup.blocksRaycasts = false;
        sidePanelCanvasGroup.interactable = false;
        sidePanelCanvasGroup.alpha = 0;
        
        sidePanel.anchoredPosition = defaultPanelPosition + panelEffectOffset;
    }
    
    
    
    // Buttons
    public void OnResumeClick()
    {
        SetInvisible();
    }
    
    public void OnSettingsClick()
    {
        SetVisible();
    }
    
    public void OnMenuClick()
    {
        
    }
}
