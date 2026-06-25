using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public enum NotificationType
    {
        None,
        Inventory,
        Journal
    }
    
    [Header("References")]
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text keybindTextLabel;
    [SerializeField] private TMP_Text keyTextLabel;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite inventorySprite;
    [SerializeField] private Sprite journalSprite;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [Header("Settings")]
    [SerializeField] private float appearDuration = .2f;
    [SerializeField] private float disappearDuration = .8f;
    [SerializeField] private float effectSizeY = 0;
    [SerializeField] private Vector2 defaultSize;
    
    
    
    // Functions
    void Awake()
    {
        Appear();
    }
    
    public void SetData(string text, NotificationType notificationType)
    {
        textLabel.text = text;
        if (notificationType != NotificationType.None)
            textLabel.text = GetNotificationText(notificationType);
        
        string keybindString = GetKeybindString(notificationType);
        string keyString = GetKeyString(notificationType);
        keybindTextLabel.text = keybindString;
        keyTextLabel.text = keyString;
        
        icon.sprite = GetSprite(notificationType);
    }

    public void Kill()
    {
        Disappear();
    }
    
    
    
    // Helpers
    private string GetNotificationText(NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.None:
                return "";
            case NotificationType.Inventory:
                return "Item added to inventory";
            case NotificationType.Journal:
                return "Entry added to Journal";
        }
        
        return "";
    }
    
    private string GetKeybindString(NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.None:
                return "";
            case NotificationType.Inventory:
                return "Press      to open Inventory";
            case NotificationType.Journal:
                return "Press      to open Journal";
        }
        
        return "";
    }
    
    private string GetKeyString(NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.None:
                return "";
            case NotificationType.Inventory:
                return GameManager.instance.settings.inventoryKey.ToString();
            case NotificationType.Journal:
                return GameManager.instance.settings.journalKey.ToString();
        }
        
        return "";
    }
    
    private Sprite GetSprite(NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.None:
                return null;
            case NotificationType.Inventory:
                return inventorySprite;
            case NotificationType.Journal:
                return journalSprite;
        }
        
        return null;
    }
    
    public void Appear()
    {
        RectTransform rectTransform = transform as RectTransform;
        
        canvasGroup.alpha = 0;
        rectTransform.sizeDelta = new Vector2(defaultSize.x, effectSizeY);
        
        canvasGroup.DOFade(1, appearDuration).SetEase(Ease.OutCubic);
        rectTransform.DOSizeDelta(defaultSize, appearDuration).SetEase(Ease.OutCubic);
    }

    private void Disappear()
    {
        canvasGroup.alpha = 1;
        
        canvasGroup.DOFade(0, disappearDuration).SetEase(Ease.OutCubic).OnComplete((() => {Destroy(gameObject);}));
    }
}
