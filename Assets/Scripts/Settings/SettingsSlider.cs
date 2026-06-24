using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    public enum SettingVariable
    {
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
            case SettingVariable.MouseSensitivity:
                slider.value = GameManager.instance.settings.mouseSensitivity;
                break;
            case SettingVariable.MasterVolume:
                slider.value = GameManager.instance.settings.masterVolume;
                break;
        }
        
        Refresh(slider.value);
        slider.onValueChanged.AddListener(Refresh);
    }

    private void Refresh(float value)
    {
        amount.text = value.ToString();
    }
}
