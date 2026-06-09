using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    enum InteractionType
    {
        Interactable,
        Pickup,
        Observation
    }
    
    [Header("Settings")]
    [SerializeField] private InteractionType interactionType = InteractionType.Interactable;
    [SerializeField] private bool destroyOnUse = false;
    private GameObject interactionPromptInstance;
    
    [Header("Events")]
    [SerializeField] private UnityEvent onInteracted;
    private void Start()
    {
        GameObject prefab = InteractionHandler.instance.interactionPrompts[(int)interactionType];
        
        interactionPromptInstance = Instantiate(prefab, transform.position, Quaternion.identity);
        interactionPromptInstance.transform.parent = transform;
        interactionPromptInstance.SetActive(false);
    }
    public void Interact()
    {
        onInteracted?.Invoke();

        if (destroyOnUse)
        {
            Destroy(interactionPromptInstance);
            Destroy(gameObject);
        }
    }
    public void ShowInteractionPrompt()
    {
        interactionPromptInstance.SetActive(true);
    }
    public void HideInteractionPrompt()
    {
        interactionPromptInstance.SetActive(false);
    }

    private void OnEnable()
    {
        InteractionHandler.instance.interactables.Add(this);
    }
    
    private void OnDisable()
    {
        InteractionHandler.instance.interactables.Remove(this);
    }
}
