using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
     [Header("References")]
     [SerializeField] private Slider brightness;
     [SerializeField] private Slider mouseSensitivity;
     [SerializeField] private Slider masterVolume;
     
     
     
     // Functions
     private void Awake()
     {
          brightness.onValueChanged.AddListener(BrightnessChanged);
          
          mouseSensitivity.onValueChanged.AddListener(MouseSensitivityChanged);
          
          masterVolume.onValueChanged.AddListener(MasterVolumeChanged);
     }
     
     
     
     // Helpers
     private void BrightnessChanged(float value)
     {
          GameManager.instance.settings.brightness = (int)value;
     }
     
     private void MouseSensitivityChanged(float value)
     {
          GameManager.instance.settings.mouseSensitivity = (int)value;
     }
     
     private void MasterVolumeChanged(float value)
     {
          GameManager.instance.settings.masterVolume = (int)value;
     }
}
