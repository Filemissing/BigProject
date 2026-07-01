using DSP;
using UnityEngine;

public class DialogueMainUIUpdater : MonoBehaviour
{
    // Event Bindings
    void OnEnable()
    {
        DSP_ConversationManager.instance.OnConversationStarted += GameManager.instance.currentMainUIManager.SetMainUI;
        DSP_ConversationManager.instance.OnConversationEnded += GameManager.instance.currentMainUIManager.RemoveMainUI;
    }
    
    void OnDisable()
    {
        DSP_ConversationManager.instance.OnConversationStarted -= GameManager.instance.currentMainUIManager.SetMainUI;
        DSP_ConversationManager.instance.OnConversationEnded -= GameManager.instance.currentMainUIManager.RemoveMainUI;
    }
}
