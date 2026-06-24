using TMPro;
using UnityEngine;

public class SettingsBool : MonoBehaviour
{
    public enum SettingVariable
    {
        ShutdownButton
    }
    
    [SerializeField] private SettingVariable setting = SettingVariable.ShutdownButton;
    [SerializeField] private TMP_Text text;
    
    
    
    // Functions
    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (GameManager.instance.settings.shutdownButton)
            text.text = "Enabled";
        else
            text.text = "Disabled";
    }
    
    public void Toggle()
    {
        switch (setting)
        {
            case SettingVariable.ShutdownButton:
                GameManager.instance.settings.shutdownButton = !GameManager.instance.settings.shutdownButton;
                break;
        }
    }
}
