using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string firstSceneName;
    
    
    
    // Buttons
    public void OnStartButton()
    {
        SceneManager.LoadScene(firstSceneName);
    }
    
    public void OnHistoryButton()
    {
        
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
        
    }
}
