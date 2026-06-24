using UnityEngine;

public class MainMenuCameraEffect : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rotationAmount = 1.5f;
    [SerializeField] private float speed = 0.2f;

    private Vector3 initialVector;

    void Start()
    {
        initialVector = transform.localEulerAngles;
    }

    void Update()
    {
        float time = Time.unscaledTime * speed;

        float xOffset = (Mathf.PerlinNoise(time, 0f) - .5f) * rotationAmount;
        float yOffset = (Mathf.PerlinNoise(0, time) - .5f) * rotationAmount;
        
        transform.rotation = Quaternion.Euler(initialVector.x + xOffset, initialVector.y + yOffset, initialVector.z);
    }
}
