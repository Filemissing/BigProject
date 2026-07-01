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
    
    public void SetMainUI()
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
    
    public void RemoveMainUI()
    {
        hasCurrentMainUI = false;
        currentMainUI = null;
        
        Refresh();
    }
}
