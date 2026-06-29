using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    public enum SettingVariable
    {
        Brightness,
        MouseSensitivity,
        MasterVolume
    }
    
    [SerializeField] private SettingVariable setting = SettingVariable.MouseSensitivity;
    [SerializeField] private TMP_Text amount;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        // Update slider value to match settings
        switch (setting)
        {
            case SettingVariable.Brightness:
                slider.value = GameManager.instance.settings.brightness;
                break;
            case SettingVariable.MouseSensitivity:
                slider.value = GameManager.instance.settings.mouseSensitivity;
                break;
            case SettingVariable.MasterVolume:
                slider.value = GameManager.instance.settings.masterVolume;
                break;
        }
        
        Refresh();
        //slider.onValueChanged.AddListener(Refresh);
    }

    public void Refresh()
    {
        amount.text = slider.value.ToString();
    }
}
