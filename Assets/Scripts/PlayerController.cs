using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    static PlayerController instance;
    private Rigidbody rb;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private Transform cameraAnchor;
    [SerializeField] private float upLookLimit = 80f;
    [SerializeField] private float downLookLimit = 80f;


    public void Update()
    {
        cameraAnchor.transform.DOKill();

        float forward = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        float currentSpeed = Input.GetKey(sprintKey) ? sprintSpeed : speed;

        Vector3 movement = (transform.forward * forward + transform.right * strafe) * currentSpeed;

        rb.linearVelocity = movement;

        Vector2 deltaMouse = Input.mousePositionDelta;
        float pitch = cameraAnchor.rotation.eulerAngles.x + -deltaMouse.y * mouseSensitivity;

        pitch = (pitch + 180f) % 360f - 180f;
        pitch = Mathf.Clamp(pitch, -upLookLimit, downLookLimit);

        float heading = transform.rotation.eulerAngles.y + deltaMouse.x * mouseSensitivity;

        transform.localRotation = Quaternion.Euler(new Vector3(0, heading, 0));
        cameraAnchor.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));
    }
}
