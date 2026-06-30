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

    [HideInInspector] public bool isSprinting;
    [HideInInspector] public bool isCrouching;

    [Header("Camera")]
    [SerializeField] private float additionalMouseSensitivity = 0.4f;
    public Transform cameraAnchor;
    [SerializeField] private float upLookLimit = 80f;
    [SerializeField] private float downLookLimit = 80f;
    
    [Header("Sound")]
    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private AudioSource crouchAudioSource;
    [SerializeField] private float stepDistance;
    [SerializeField] private float sprintStepDistance;
    [SerializeField] private float crouchStepDistance;
    [SerializeField] private float sprintVolumeMultiplier;
    [SerializeField] private float crouchVolumeMultiplier;


    private bool canMove = true;
    private bool canLook = true;
    private bool unCrouched = false;

    private Vector3 lastStepPosition = Vector3.zero;
    private bool previousCanStep = false;
    private bool canStep = false;
    
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

            isSprinting = Input.GetKey(GameManager.instance.settings.sprintKey);
            isCrouching = Input.GetKey(GameManager.instance.settings.crouchKey);
            
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
                
                // Sound
                crouchAudioSource.Play();
            }
            else if (unCrouched)
            {
                transform.DOScaleY(1f, crouchTransitionDuration);
                unCrouched = false;
                
                // Sound
                crouchAudioSource.Play();
            }

            if (Input.GetKeyDown(GameManager.instance.settings.kickKey))
            {
                bool success = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f);
                if (success && hit.rigidbody)
                {
                    hit.rigidbody.isKinematic = false;
                    hit.rigidbody.AddForce(cameraAnchor.forward * 100f, ForceMode.Impulse);
                }
            }

			// Sound
            canStep = currentSpeed > 0;
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
        
        // Sound
        if (!canStep) return;
        
        if (canStep != previousCanStep && previousCanStep == false) // Started moving
            lastStepPosition = transform.position;
        
        float distance = Vector3.Distance(lastStepPosition, transform.position);
        if (isSprinting && !isCrouching) // Sprinting
        {
            if (distance >= sprintStepDistance)
            {
                stepAudioSource.PlayOneShot(stepAudioSource.clip, sprintVolumeMultiplier);
                lastStepPosition = transform.position;
            }
        }
        else if (!isSprinting && isCrouching) // Crouching
        {
            if (distance >= crouchStepDistance)
            {
                stepAudioSource.PlayOneShot(stepAudioSource.clip, crouchVolumeMultiplier);
                lastStepPosition = transform.position;
            }
        }
        else if (!isSprinting && !isCrouching) // Walking
        {
            if (distance >= stepDistance)
            {
                stepAudioSource.PlayOneShot(stepAudioSource.clip, 1);
                lastStepPosition = transform.position;
            }
        }

        previousCanStep = canStep;
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
