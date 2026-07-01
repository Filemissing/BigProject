using DSP;
using UnityEngine;

public class CollisionDialogueTrigger : MonoBehaviour
{
    [SerializeField] DSP_ConversationGraphAsset conversationGraph;
    [SerializeField] float cooldownTime = 5f;

    private float lastTriggerTime = -Mathf.Infinity;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastTriggerTime >= cooldownTime)
            {
                lastTriggerTime = Time.time;
                DSP_ConversationManager.instance.StartConversation(conversationGraph);
            }
        }
    }
}
