using System;
using NaughtyAttributes;
using UnityEngine;

public class TransitionPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    
    
    // Functions
    [Button]
    public float PlaySleepEnterAnimation()
    {
        animator.Play("TransitionSleepEnter");
        
        AnimationClip anim =  Array.Find(animator.runtimeAnimatorController.animationClips, c => c.name == "TransitionSleepEnter");
        if (anim != null)
            return anim.length;
        
        return 0f;
    }
    
    [Button]
    public float PlaySleepExitAnimation()
    {
        animator.Play("TransitionSleepExit");
        
        AnimationClip anim =  Array.Find(animator.runtimeAnimatorController.animationClips, c => c.name == "TransitionSleepExit");
        if (anim != null)
            return anim.length;
        
        return 0f;
    }
}
