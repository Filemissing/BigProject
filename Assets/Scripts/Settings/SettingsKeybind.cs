using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsKeybind : MonoBehaviour
{
    public enum SettingVariable
    {
        Interact,
        Sprint,
        Crouch,
        Kick,
        Inventory,
        Journal,
        Pause
    }
    
    [SerializeField] private SettingVariable setting = SettingVariable.Interact;
    [SerializeField] private TMP_Text text;
    
    
    
    // Functions
    private void Awake()
    {
        Refresh();
    }

    private void Refresh()
    {
        switch (setting)
        {
            case SettingVariable.Interact:
                text.text = GameManager.instance.settings.interactKey.ToString();
                break;
            case SettingVariable.Sprint:
                text.text = GameManager.instance.settings.sprintKey.ToString();
                break;
            case SettingVariable.Crouch:
                text.text = GameManager.instance.settings.crouchKey.ToString();
                break;
            case SettingVariable.Kick:
                text.text = GameManager.instance.settings.kickKey.ToString();
                break;
            case SettingVariable.Inventory:
                text.text = GameManager.instance.settings.inventoryKey.ToString();
                break;
            case SettingVariable.Journal:
                text.text = GameManager.instance.settings.journalKey.ToString();
                break;
            case SettingVariable.Pause:
                text.text = GameManager.instance.settings.pauseKey.ToString();
                break;
        }
    }
    
    public void OnClick()
    {
        StartCoroutine(Hello());

        IEnumerator Hello()
        {
            while (true)
            {
                if (Input.anyKeyDown)
                {
                    foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(key))
                        {
                            switch (setting)
                            {
                                case SettingVariable.Interact:
                                    GameManager.instance.settings.interactKey = key;
                                    break;
                                case SettingVariable.Sprint:
                                    GameManager.instance.settings.sprintKey = key;
                                    break;
                                case SettingVariable.Crouch:
                                    GameManager.instance.settings.crouchKey = key;
                                    break;
                                case SettingVariable.Kick:
                                    GameManager.instance.settings.kickKey = key;
                                    break;
                                case SettingVariable.Inventory:
                                    GameManager.instance.settings.inventoryKey = key;
                                    break;
                                case SettingVariable.Journal:
                                    GameManager.instance.settings.journalKey = key;
                                    break;
                                case SettingVariable.Pause:
                                    GameManager.instance.settings.pauseKey = key;
                                    break;
                            }

                            Refresh();
                            
                            yield break;
                        }
                    }
                }
                
                yield return null;
            }
        }
    }
}
