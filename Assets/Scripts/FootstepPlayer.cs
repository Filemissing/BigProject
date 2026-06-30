using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float stepDistance;
    [SerializeField] private AudioClip[] footstepSounds;
    
    private Vector3 lastStepPosition = Vector3.zero;



    void Update()
    {
        float distance = Vector3.Distance(lastStepPosition, transform.position);
        if (distance >= stepDistance)
        {
            audioSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)], 1);
            lastStepPosition = transform.position;
        }
    }

    void Awake()
    {
        lastStepPosition = transform.position;
    }
}
