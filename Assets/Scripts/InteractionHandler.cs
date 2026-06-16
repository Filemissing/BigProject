using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public static InteractionHandler instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private KeyCode key = KeyCode.E;
    [SerializeField] private float radius = 2f;
    public GameObject[] interactionPrompts;
    [HideInInspector] public List<Interactable> interactables = new List<Interactable>();

    void Update()
    {
        Interactable nearest = interactables.OrderBy(i => Vector3.Distance(i.transform.position, transform.position)).FirstOrDefault();

        if (nearest == null)        
            return;

        foreach (var interactable in interactables)
        {
            if (interactable == nearest && Vector3.Distance(interactable.transform.position, transform.position) <= radius)
            {
                nearest.ShowInteractionPrompt();
            }
            else
            {
                interactable.HideInteractionPrompt();
            }
        }

        if (Input.GetKeyDown(key) && Vector3.Distance(nearest.transform.position, transform.position) <= radius)
        {
            nearest?.Interact();
        }
    }
}
