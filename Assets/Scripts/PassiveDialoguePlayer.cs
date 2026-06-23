using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PassiveDialoguePlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NPC npc;
    
    [Header("Dialogues")]
    [SerializeField] private List<string> passiveDialogueEntryDatas = new List<string>();
    
    [Header("Settings")]
    [SerializeField] private float maximumDistance = 14;
    [SerializeField] private float minimumTime = 4;
    [SerializeField] private float maximumTime = 12;
    
    private int lastPlayedDialogueEntryIndex = -1;
    
    
    
    // Functions
    void Awake()
    {
        if (passiveDialogueEntryDatas.Count == 0) return;
        
        StartCoroutine(DelayedPlayDialogue());
        IEnumerator DelayedPlayDialogue()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetRandomDelay());
                PlayRandomDialogue();
            }
        }
    }
    
    
    
    // Helpers
    public void PlayRandomDialogue()
    {
        if (passiveDialogueEntryDatas.Count == 0) return;
        
        int index = 0;
        
        // Get index
        if (passiveDialogueEntryDatas.Count == 1)
            index = 0;
        else
        {
            bool isPreviousEntry = true;
            while (isPreviousEntry)
            {
                index = GetRandomIndex();
                isPreviousEntry = index == lastPlayedDialogueEntryIndex;
            }
        }
        lastPlayedDialogueEntryIndex = index;
        
        // Play dialogue
        float distance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (distance <= maximumDistance)
            DSP_ConversationManager.instance.PlayPassiveDialogue(npc.characterAsset, passiveDialogueEntryDatas[index]);
    }
    
    private int GetRandomIndex()
    {
        int maxValue = passiveDialogueEntryDatas.Count;
        return Random.Range(0, maxValue);
    }

    private float GetRandomDelay()
    {
        return Random.Range(minimumTime, maximumTime);
    }
}
