using DSP;
using System.Collections;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] DSP_ConversationGraphAsset conversation;

    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        DSP_ConversationManager.instance.StartConversation(conversation);
    }
}
