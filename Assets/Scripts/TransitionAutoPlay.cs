using UnityEngine;

public class TransitionAutoPlay : MonoBehaviour
{
    [SerializeField] private TransitionPlayer transitionPlayer;

    void Start()
    {
        transitionPlayer.PlaySleepExitAnimation();
    }
}
