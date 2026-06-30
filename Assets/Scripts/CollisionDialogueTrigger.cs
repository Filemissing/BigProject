using DSP;
using UnityEngine;

public class CollisionDialogueTrigger : MonoBehaviour
{
    [SerializeField] DSP_ConversationGraphAsset conversationGraph;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DSP_ConversationManager.instance.StartConversation(conversationGraph);
        }
    }
}
