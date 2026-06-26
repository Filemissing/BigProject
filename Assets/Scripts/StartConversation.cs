using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StartConversation : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;
    [SerializeField] float delay = 1;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }
    
    IEnumerator DelayedStart()
     {
         yield return new WaitForSeconds(delay);
         unityEvent.Invoke();
     }
}
