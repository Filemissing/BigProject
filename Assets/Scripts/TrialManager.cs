using System.Collections;
using UnityEngine;

public class TrialManager : MonoBehaviour
{
    public DSP_ConversationGraphAsset trialConversation;

    void Start()
    {
        PlayerController.instance.LockCharacter();
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        DSP_ConversationManager.instance.StartConversation(trialConversation);
    }
}
