using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class FindPlayer : MonoBehaviour
{
    CinemachineCamera cinemachineCamera;
    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    void Start()
    {
        if (cinemachineCamera.Target.TrackingTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                cinemachineCamera.Target.TrackingTarget = player.GetComponent<PlayerController>().cameraAnchor;
            }
            else
            {
                Debug.LogWarning("No GameObject with tag 'Player' found in the scene.");
            }
        }
    }
}
