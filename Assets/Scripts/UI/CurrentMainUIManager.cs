using UnityEngine;
using DSP;

public class CurrentMainUIManager : MonoBehaviour
{
    public bool hasCurrentMainUI;
    public VisibilityManager currentMainUI;


    
    void Refresh()
    {
        if (hasCurrentMainUI)
            PlayerController.instance.LockCharacter();
        else
            PlayerController.instance.UnlockCharacter();
    }

    bool DisablePreviousMainUI()
    {
        if (!currentMainUI && hasCurrentMainUI) // Previous is DSP
            return false;
        else // Previous is not DSP
        {
            if (currentMainUI) currentMainUI.TurnInvisible();
            return true;
        }
    }
    
    public bool SetMainUI(VisibilityManager mainUI)
    {
        if (!DisablePreviousMainUI()) {mainUI.TurnInvisible(); return false;}
        
        hasCurrentMainUI = true;
        currentMainUI = mainUI;
        
        Refresh();
        
        return true;
    }
    
    void SetMainUI()
    {
        if (!DisablePreviousMainUI()) return;
        
        hasCurrentMainUI = true;
        currentMainUI = null;
        
        Refresh();
    }

    public void RemoveMainUI(VisibilityManager mainUI)
    {
        if (currentMainUI != mainUI) return;
        
        hasCurrentMainUI = false;
        currentMainUI = null;
        
        Refresh();
    }
    
    void RemoveMainUI()
    {
        hasCurrentMainUI = false;
        currentMainUI = null;
        
        Refresh();
    }
    
    
    
    // Event Bindings
    void OnEnable()
    {
        DSP_ConversationManager.instance.OnConversationStarted += SetMainUI;
        DSP_ConversationManager.instance.OnConversationEnded += RemoveMainUI;
    }
    
    void OnDisable()
    {
        DSP_ConversationManager.instance.OnConversationStarted -= SetMainUI;
        DSP_ConversationManager.instance.OnConversationEnded -= RemoveMainUI;
    }
}
