using System;
using UnityEngine;

public class HandleIsBusyOnDialogue : MonoBehaviour
{
    void IsBusyEnable()
    {
        InteractionHandler.instance.isBusy = true;
    }
    
    void IsBusyDisable()
    {
        InteractionHandler.instance.isBusy = false;
    }

    // Event bindings
    private void OnEnable()
    {
        DSP_ConversationManager.instance.OnConversationStarted += IsBusyEnable;
        DSP_ConversationManager.instance.OnConversationEnded += IsBusyDisable;
    }

    private void OnDisable()
    {
        DSP_ConversationManager.instance.OnConversationStarted -= IsBusyEnable;
        DSP_ConversationManager.instance.OnConversationEnded -= IsBusyDisable;
    }
}
