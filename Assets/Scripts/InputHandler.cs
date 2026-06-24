using System;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    // Events
    public event Action OnInventoryToggle;
    public event Action onJournalToggle;
    public event Action onPauseToggle;

    void Update()
    {
        if (Input.GetKeyDown(GameManager.instance.settings.inventoryKey)) OnInventoryToggle?.Invoke();
        if (Input.GetKeyDown(GameManager.instance.settings.journalKey)) onJournalToggle?.Invoke();
        if (Input.GetKeyDown(GameManager.instance.settings.pauseKey)) onPauseToggle?.Invoke();
    }
}
