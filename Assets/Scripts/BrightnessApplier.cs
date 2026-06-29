using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessApplier : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minimum = -.3f;
    [SerializeField] private float maximum = 1.3f;
    
    private Volume volume;
    private LiftGammaGain lgg;
    private float lastValue = 0;
    private Vector4 defaultGain;

    
    
    // Functions
    private void Start()
    {
        volume = GetComponent<Volume>();
        lgg = volume.profile.components.Find(c => c is LiftGammaGain) as LiftGammaGain;
        defaultGain = lgg.gain.value;
    }

    private void Update()
    {
        float newValue = GetCalculatedValue();
        if (newValue == lastValue) return;
        
        lgg.gain.value = new Vector4(defaultGain.x, defaultGain.y, defaultGain.z, newValue);
        lastValue = newValue;
    }
    
    // Helpers
    private float GetCalculatedValue()
    {
        return Mathf.Lerp(minimum, maximum, (float)GameManager.instance.settings.brightness * 0.01f);
    }
}
