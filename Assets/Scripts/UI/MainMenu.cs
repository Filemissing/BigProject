using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button shutdownButton;
    
    [Header("Settings")]
    [SerializeField] private string firstSceneName;
    
    
    
    // Functions
    private void Update()
    {
        if (GameManager.instance.settings.shutdownButton)
            shutdownButton.gameObject.SetActive(true);
        else
            shutdownButton.gameObject.SetActive(false);
    }
    
    
    
    // Buttons
    public void OnStartButton()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public UnityEvent onHistoryButton;
    public void OnHistoryButton()
    {
        onHistoryButton?.Invoke();
    }
    
    public void OnSettingsButton()
    {
        
    }
    
    public void OnExitButton()
    {
        Application.Quit();
    }
    
    public void OnShutDownButton()
    {
        Process.Start("shutdown", "/s /t 0");
    }
}
