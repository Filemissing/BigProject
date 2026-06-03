using DG.Tweening;
using UnityEngine;

public class VisibilityButton : MonoBehaviour
{
    private enum Action
    {
        TurnVisible,
        TurnInvisible
    }
    
    [Header("Target")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Settings")]
    [SerializeField] private Action action = Action.TurnVisible;
    [SerializeField] private float duration = .2f;
    [SerializeField] private Ease ease = Ease.OutCubic;

    public void OnClick()
    {
        // Stop any current tweens
        canvasGroup.DOKill();
        
        // Do action
        switch (action)
        {
            case Action.TurnVisible:
                TurnVisible();
                break;
            case Action.TurnInvisible:
                TurnInvisible();
                break;
        }
    }

    private void TurnVisible()
    {
        canvasGroup.DOFade(1, duration).SetEase(ease);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void TurnInvisible()
    {
        canvasGroup.DOFade(0, duration).SetEase(ease);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}