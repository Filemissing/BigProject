using TMPro;
using UnityEngine;

public class InteractionPromptKeyUpdate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text text;
    
    
    
    // Functions
    void Update() // Don't look here
    {
        text.text = GameManager.instance.settings.interactKey.ToString();
    }
}
