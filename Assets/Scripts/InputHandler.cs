using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode inventoryToggle = KeyCode.I;
    [SerializeField] private KeyCode journalToggle = KeyCode.J;
    [SerializeField] private KeyCode TEMP_CursorUnlock = KeyCode.LeftAlt;
    
    // Events
    public event Action OnInventoryToggle;
    public event Action onJournalToggle;

    void Update()
    {
        if (Input.GetKeyDown(inventoryToggle)) OnInventoryToggle?.Invoke();
        if (Input.GetKeyDown(journalToggle)) onJournalToggle?.Invoke();
        
        // Temporary cursor unlock.     - MICHA WHY DOES UNLOCKCHARACTER LOCK THE CURSOR???
        if (Input.GetKeyDown(TEMP_CursorUnlock))
        {
            if (Cursor.lockState == CursorLockMode.None)
                GameManager.instance.player.UnlockCharacter();
            else
                GameManager.instance.player.LockCharacter();
        }
    }
}
