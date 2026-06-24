using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup sidePanelCanvasGroup;
    [SerializeField] private RectTransform sidePanel;
    [SerializeField] private CanvasGroup darkEffectCanvasGroup;
    [SerializeField] private VisibilityManager settings;

    [Header("Settings")]
    [SerializeField] private float duration = .3f;
    [SerializeField] private Vector2 panelEffectOffset = new  Vector2(-200, 0);

    private bool toggle = false;
    private Vector2 defaultPanelPosition;
    
    
    
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
        // Freeze
        Time.timeScale = 0;
        
        // Character Lock Management
        GameManager.instance.player.LockCharacter();
        
        // Hide Settings
        settings.TurnInvisible();
        
        // Effect
        darkEffectCanvasGroup.blocksRaycasts = true;
        darkEffectCanvasGroup.interactable = true;
        darkEffectCanvasGroup.DOFade(1, duration).SetEase(Ease.OutCubic).SetUpdate(true);
        
        sidePanelCanvasGroup.blocksRaycasts = true;
        sidePanelCanvasGroup.interactable = true;
        sidePanelCanvasGroup.DOFade(1, duration).SetEase(Ease.OutCubic).SetUpdate(true);
        
        sidePanel.anchoredPosition = defaultPanelPosition + panelEffectOffset;
        sidePanel.DOAnchorPos(defaultPanelPosition, duration).SetEase(Ease.OutCubic).SetUpdate(true);
    }
    
    private void SetInvisible()
    {
        // Unfreeze
        Time.timeScale = 1;
        
        // Cursor Lock Management
        if (GameManager.instance.currentMainUIManager.hasCurrentMainUI)
            GameManager.instance.player.LockCharacter();
        else
            GameManager.instance.player.UnlockCharacter();
        
        // Hide Settings
        settings.TurnInvisible();
        
        // Effect
        darkEffectCanvasGroup.blocksRaycasts = false;
        darkEffectCanvasGroup.interactable = false;
        darkEffectCanvasGroup.DOFade(0, duration).SetEase(Ease.OutCubic).SetUpdate(true);
        
        sidePanelCanvasGroup.blocksRaycasts = false;
        sidePanelCanvasGroup.interactable = false;
        sidePanelCanvasGroup.DOFade(0, duration).SetEase(Ease.OutCubic).SetUpdate(true);
        
        sidePanel.anchoredPosition = defaultPanelPosition;
        sidePanel.DOAnchorPos(defaultPanelPosition + panelEffectOffset, duration).SetEase(Ease.OutCubic).SetUpdate(true);
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
        
    }
    
    public void OnSettingsClick()
    {
        
    }
    
    public void OnMenuClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    
    
    
    // Event Bindings
    private void OnEnable()
    {
        GameManager.instance.inputHandler.onPauseToggle += ToggleVisibility;
    }

    private void OnDisable()
    {
        GameManager.instance.inputHandler.onPauseToggle -= ToggleVisibility;
    }
}
