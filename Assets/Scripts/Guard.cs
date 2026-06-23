using System;
using UnityEngine;

public class Guard : NPC
{
    [SerializeField] float FOV = 45;
    [SerializeField] float visionDistance;
    [SerializeField] float crouchedVisionDistance;
    [SerializeField] float timeToNotice = 1f;
    [SerializeField] float timeToGiveUp = 5f;

    [Header("Guard")]
    public AlertStatus alertStatus = AlertStatus.Unaware;
    [SerializeField] Transform playerSetBackPoint;

    private PlayerController player;
    private void Start()
    {
        // taken from NPC start since it gets overriden by unity
        StartCoroutine(Wander());
        DSP_ConversationManager.instance.OnConversationEnded += ResumeWandering;

        player = PlayerController.instance;

        // adjust wander parameters to fit a more patrol-like style
        keepOrder = true;
        stayDurationRange = new Vector2(5f, 5f);
        rotationTime = .01f;
    }

    float visibleTimer = 0f;
    float searchingTimer = 0f;
    float suspiciousTimer = 0f;

    Vector3 suspiciousPoint;
    private void Update()
    {
        // adjust state based on vision
        Vector3 toPlayer = player.transform.position - transform.position;

        toPlayer = Vector3.ProjectOnPlane(toPlayer, Vector3.up); // only XZ plane
        toPlayer.Normalize();

        float angle = Vector3.Angle(transform.forward, toPlayer); // unsigned

        float distance = Vector3.Distance(transform.position, player.transform.position);
        float visibleDistance = player.isCrouched ? crouchedVisionDistance : visionDistance;

        if (angle < FOV && distance < visibleDistance)
        {
            Physics.Raycast(transform.position + Vector3.up, toPlayer, out RaycastHit hit, visibleDistance);
            if (hit.collider?.gameObject && hit.collider?.gameObject == player.gameObject)
            {
                if (alertStatus == AlertStatus.Unaware)
                {
                    alertStatus = AlertStatus.Suspicious;
                    suspiciousPoint = player.transform.position;
                }

                // give the player some time to hide if they were crouching
                if (alertStatus == AlertStatus.Suspicious)
                {
                    if (player.isCrouched)
                        visibleTimer += Time.deltaTime;
                    else
                        alertStatus = AlertStatus.Found;
                }
                if (visibleTimer > timeToNotice)
                {
                    visibleTimer = 0f;
                    alertStatus = AlertStatus.Found;
                }

                // if they were searhing they will notice immediately no matter what
                if (alertStatus == AlertStatus.Searching)
                    alertStatus = AlertStatus.Found;

                if (alertStatus == AlertStatus.Found)
                    suspiciousPoint = player.transform.position; 
            }
        }
        else // player is not visible
        {
            // player just left the field of view
            if (alertStatus == AlertStatus.Found)
                alertStatus = AlertStatus.Searching;

            if (alertStatus == AlertStatus.Searching)
                searchingTimer += Time.deltaTime;
            if (searchingTimer > timeToGiveUp)
            {
                alertStatus = AlertStatus.Suspicious;
                suspiciousPoint = player.transform.position;
            }

            if (alertStatus == AlertStatus.Suspicious) 
                suspiciousTimer += Time.deltaTime;
            if (suspiciousTimer > timeToGiveUp)
                alertStatus = AlertStatus.Unaware;
        }

        // act based on state
        if (alertStatus == AlertStatus.Unaware)
        {
            agent.speed = 3.5f;
            agent.acceleration = 8;
            agent.angularSpeed = 360;
            ResumeWandering();
            return; // handled by wander loop in NPC
        }

        // pause wandering but don't stop the agent
        pauseWandering = true;
        shouldRotate = false;

        if (alertStatus == AlertStatus.Suspicious)
        {
            agent.isStopped = true;
            RotateToward(suspiciousPoint);
        }

        if (alertStatus == AlertStatus.Searching || alertStatus == AlertStatus.Found)
        {
            // works for both Searching and Found, the difference is in updating the suspiciousPoint
            agent.speed = 12;
            agent.acceleration = 500;
            agent.angularSpeed = 3000;
            agent.destination = suspiciousPoint;
            
            if (HasAgentArrived())
            {
                RotateToward(player.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayDialogue();
            player.transform.position = playerSetBackPoint.position;
        }
    }

    public enum AlertStatus
    {
        Unaware,
        Suspicious, // seen shortly or heard
        Searching, // no longer visible
        Found
    }
}
