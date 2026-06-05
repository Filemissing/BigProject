using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private GameObject interactionPromptInstance;
    private void Start()
    {
        interactionPromptInstance = Instantiate(GameManager.instance.interactionPromptPrefab, transform.position, Quaternion.identity, transform);
        interactionPromptInstance.SetActive(false);
    }

    [SerializeField] private UnityEvent onInteracted;
    public void Interact()
    {
        onInteracted?.Invoke();
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
