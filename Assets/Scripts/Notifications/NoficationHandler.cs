using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoficationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Notification notificationPrefab;
    
    [Header("Settings")]
    [SerializeField] private float lifeTime = 6;
    
    private List<Notification> currentNotifications = new List<Notification>();
    
    
    
    // Functions
    public void CreateNotification(string text, Notification.NotificationType notificationType)
    {
        Notification notification = Instantiate(notificationPrefab, transform);
        notification.SetData(text, notificationType);
        
        notification.Appear();

        StartCoroutine(KillAfterTime(notification));
    }
    
    
    
    // Helpers
    IEnumerator KillAfterTime(Notification notification)
    {
        yield return new WaitForSeconds(lifeTime);
        
        currentNotifications.Remove(notification);
        
        notification?.Kill();
    }

    void OnItemAdded()
    {
        CreateNotification("", Notification.NotificationType.Inventory);
    }

    void OnJournalUpdated()
    {
        CreateNotification("", Notification.NotificationType.Journal);
    }
    
    
    
    // Event Bindings
    void OnEnable()
    {
        GameManager.instance.inventoryData.OnItemAdded += OnItemAdded;
        GameManager.instance.journalData.JournalUpdated += OnJournalUpdated;
    }
    
    void OnDisable()
    {
        GameManager.instance.inventoryData.OnItemAdded -= OnItemAdded;
        GameManager.instance.journalData.JournalUpdated -= OnJournalUpdated;
    }
}
