using UnityEngine;
using UnityEngine.Events;

public class StartConversation : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;

    void Start()
    {
        unityEvent.Invoke();
    }
}
