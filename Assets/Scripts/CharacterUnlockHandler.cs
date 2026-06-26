using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DSP;

public class CharacterUnlockHandler : MonoBehaviour
{
    public int characterUnlockBlocks;



    void Refresh()
    {
        if (characterUnlockBlocks > 0)
            PlayerController.instance.LockCharacter();
        else
            PlayerController.instance.UnlockCharacter();
    }
    
    public void AddBlock()
    {
        characterUnlockBlocks++;
        characterUnlockBlocks = math.clamp(characterUnlockBlocks, 0, int.MaxValue);
        Refresh();
    }

    public void RemoveBlock()
    {
        characterUnlockBlocks--;
        characterUnlockBlocks = math.clamp(characterUnlockBlocks, 0, int.MaxValue);
        Refresh();
    }
    
    
    
    // Event Bindings
    void OnEnable()
    {
        DSP_ConversationManager.instance.OnConversationStarted += AddBlock;
        DSP_ConversationManager.instance.OnConversationEnded += RemoveBlock;
    }
    
    void OnDisable()
    {
        DSP_ConversationManager.instance.OnConversationStarted -= AddBlock;
        DSP_ConversationManager.instance.OnConversationEnded -= RemoveBlock;
    }
}
