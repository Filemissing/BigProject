using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
     [Header("References")]
     [SerializeField] private Slider mouseSensitivity;
     [SerializeField] private Slider masterVolume;
     
     
     
     // Functions
     private void Awake()
     {
          mouseSensitivity.onValueChanged.AddListener(MouseSensitivityChanged);
          
          masterVolume.onValueChanged.AddListener(MasterVolumeChanged);
     }
     
     
     
     // Helpers
     private void MouseSensitivityChanged(float value)
     {
          GameManager.instance.settings.mouseSensitivity = (int)value;
     }
     
     private void MasterVolumeChanged(float value)
     {
          GameManager.instance.settings.masterVolume = (int)value;
     }
}
