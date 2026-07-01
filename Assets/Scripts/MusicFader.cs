using System;
using DG.Tweening;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    
    [Header("Settings")]
    [SerializeField] private float fadeInDuration = 0.2f;
    [SerializeField] private float fadeOutDuration = 0.2f;

    private float defaultVolume;

    
    
    // Functions
    void Start()
    {
        defaultVolume = audioSource.volume;
    }
    
    public void PlayFade()
    {
        audioSource.DOKill();
        audioSource.volume = 0;
        
        audioSource.Play();
        audioSource.DOFade(defaultVolume, fadeInDuration).SetEase(Ease.OutCubic).SetUpdate(true);
    }
    
    public void StopFade()
    {
        audioSource.DOKill();
        audioSource.volume = defaultVolume;
        
        audioSource.DOFade(0, fadeOutDuration).SetEase(Ease.OutCubic).SetUpdate(true);
    }
}
