using DG.Tweening;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{
    private enum TurnVisibleAnimation
    {
        Fade
    }
    
    private enum TurnInvisibleAnimation
    {
        Fade
    }

    private enum EventType
    {
        None,
        Inventory,
        Journal
    }
    
    [Header("Target")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Settings")]
    [SerializeField] private EventType eventType;
    [SerializeField] private TurnVisibleAnimation turnVisibleAnimation;
    [SerializeField] private TurnInvisibleAnimation turnInvisibleAnimation;
    [SerializeField] private float duration = .2f;

    private bool toggle = false;



    void Awake()
    {
        ForceInvisible();
    }
    
    // Public Functions
    public void TurnVisible()
    {
        switch (turnVisibleAnimation)
        {
            case TurnVisibleAnimation.Fade:
                TurnVisibleFade();
                break;
        }
    }

    public void TurnInvisible()
    {
        switch (turnInvisibleAnimation)
        {
            case TurnInvisibleAnimation.Fade:
                TurnInvisibleFade();
                break;
        }
    }

    public void ForceInvisible()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    
    
    
    // Helpers
    private void ToggleVisibility()
    {
        if (toggle)
            TurnInvisible();
        else
            TurnVisible();
        
        toggle = !toggle;
    }
    
    
    
    // TurnVisible Effects
    private void TurnVisibleFade()
    {
        // Stop any current tweens
        canvasGroup.DOKill();
        
        canvasGroup.DOFade(1, duration).SetEase(Ease.OutCubic);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    
    
    // TurnInvisible Effects
    private void TurnInvisibleFade()
    {
        // Stop any current tweens
        canvasGroup.DOKill();
        
        canvasGroup.DOFade(0, duration).SetEase(Ease.OutCubic);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    
    
    
    // Event Bindings
    void OnEnable()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.Inventory:
                GameManager.instance.inputHandler.OnInventoryToggle += ToggleVisibility;
                break;
            case EventType.Journal:
                GameManager.instance.inputHandler.onJournalToggle += ToggleVisibility;
                break;
        }
    }
    
    void OnDisable()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.Inventory:
                GameManager.instance.inputHandler.OnInventoryToggle -= ToggleVisibility;
                break;
            case EventType.Journal:
                GameManager.instance.inputHandler.onJournalToggle -= ToggleVisibility;
                break;
        }
    }
}
