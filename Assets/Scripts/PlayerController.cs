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
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private float crouchScale = 0.5f;
    [SerializeField] private float crouchTransitionDuration = 0.2f;
    [SerializeField] private float crouchSpeed = 3f;

    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    public Transform cameraAnchor;
    [SerializeField] private float upLookLimit = 80f;
    [SerializeField] private float downLookLimit = 80f;


    public void Update()
    {
        cameraAnchor.transform.DOKill();

        float forward = Input.GetAxisRaw("Vertical");
        float strafe = Input.GetAxisRaw("Horizontal");

        float currentSpeed = Input.GetKey(sprintKey) ? sprintSpeed : speed;
        if (Input.GetKey(crouchKey))
        {
            currentSpeed = crouchSpeed;
        }

        Vector3 movement = (transform.forward * forward + transform.right * strafe).normalized * currentSpeed;
        movement.y = rb.linearVelocity.y;

        rb.linearVelocity = movement;

        Vector2 deltaMouse = Input.mousePositionDelta;
        float pitch = cameraAnchor.rotation.eulerAngles.x + -deltaMouse.y * mouseSensitivity;

        pitch = (pitch + 180f) % 360f - 180f;
        pitch = Mathf.Clamp(pitch, -upLookLimit, downLookLimit);

        float heading = transform.rotation.eulerAngles.y + deltaMouse.x * mouseSensitivity;

        transform.localRotation = Quaternion.Euler(new Vector3(0, heading, 0));
        cameraAnchor.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));

        if (Input.GetKeyDown(crouchKey))
        {
            transform.DOScaleY(crouchScale, crouchTransitionDuration);
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            transform.DOScaleY(1f, crouchTransitionDuration);
        }
    }
}
