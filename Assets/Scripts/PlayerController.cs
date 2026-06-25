using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
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
        UnlockCharacter();
    }

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float crouchScale = 0.5f;
    [SerializeField] private float crouchTransitionDuration = 0.2f;
    [SerializeField] private float crouchSpeed = 3f;

    [HideInInspector] public bool isCrouched;

    [Header("Camera")]
    [SerializeField] private float additionalMouseSensitivity = 0.4f;
    public Transform cameraAnchor;
    [SerializeField] private float upLookLimit = 80f;
    [SerializeField] private float downLookLimit = 80f;


    bool canMove = true;
    bool canLook = true;
    bool unCrouched = false;
    
    public void Update()
    {
        // keep outside canMove check to mitigate permanent crouch after dialogue
        if (Input.GetKeyUp(GameManager.instance.settings.crouchKey))
            unCrouched = true;

        cameraAnchor.transform.DOKill();

        if (canMove)
        {
            float forward = Input.GetAxisRaw("Vertical");
            float strafe = Input.GetAxisRaw("Horizontal");

            float currentSpeed = Input.GetKey(GameManager.instance.settings.sprintKey) ? sprintSpeed : speed;
            if (Input.GetKey(GameManager.instance.settings.crouchKey))
            {
                currentSpeed = crouchSpeed;
            }

            Vector3 movement = (transform.forward * forward + transform.right * strafe).normalized * currentSpeed;
            movement.y = rb.linearVelocity.y;

            rb.linearVelocity = movement;

            if (Input.GetKeyDown(GameManager.instance.settings.crouchKey))
            {
                transform.DOScaleY(crouchScale, crouchTransitionDuration);
            }
            else if (unCrouched)
            {
                transform.DOScaleY(1f, crouchTransitionDuration);
                unCrouched = false;
            }

            if (Input.GetKeyDown(GameManager.instance.settings.kickKey))
            {
                Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f);
                if (!hit.collider && hit.rigidbody.gameObject.TryGetComponent(out NavMeshAgent navMeshAgent))
                {
                    hit.rigidbody.isKinematic = false;
                    hit.rigidbody.AddForce(cameraAnchor.forward * 100f, ForceMode.Impulse);
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (canLook)
        {
            Vector2 deltaMouse = Input.mousePositionDelta;
            float pitch = cameraAnchor.rotation.eulerAngles.x + -deltaMouse.y * additionalMouseSensitivity * ((float)GameManager.instance.settings.mouseSensitivity / 100);

            pitch = (pitch + 180f) % 360f - 180f;
            pitch = Mathf.Clamp(pitch, -upLookLimit, downLookLimit);

            float heading = transform.rotation.eulerAngles.y + deltaMouse.x * additionalMouseSensitivity * ((float)GameManager.instance.settings.mouseSensitivity / 100);

            transform.localRotation = Quaternion.Euler(new Vector3(0, heading, 0));
            cameraAnchor.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void LockCharacter()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        canMove = false;
        canLook = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void UnlockCharacter()
    {
        canMove = true;
        canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
