using DG.Tweening;
using NaughtyAttributes;
using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class NPC : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Rigidbody rb;
    protected Animator animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    [Header("Dialogue")]
    public DSP_CharacterAsset characterAsset;
    [SerializeField] private DSP_ConversationGraphAsset[] conversations;

    private int currentConversationIndex = 0;

    public void PlayDialogue()
    {
        PauseWandering();

        RotateToward(PlayerController.instance.transform.position);

        if (conversations != null && conversations.Length > 0)
        {
            DSP_ConversationGraphAsset conversation = conversations[currentConversationIndex];
            DSP_ConversationManager.instance.StartConversation(conversation);
        }

        // last conversation will be repeated
        if (currentConversationIndex < conversations.Length - 1)
        {
            currentConversationIndex++;
        }
    }

    [Header("Wandering")]
    [SerializeField] private List<PointOfInterest> pointsOfInterest;

    private void Start()
    {
        if (pointsOfInterest.Count > 0)
            StartCoroutine(Wander());
        
    }

    private void OnEnable()
    {
        DSP_ConversationManager.instance.OnConversationEnded += ResumeWandering;
    }

    private void OnDisable()
    {
        DSP_ConversationManager.instance.OnConversationEnded -= ResumeWandering;
    }

    protected bool pauseWandering = false;
    protected bool shouldRotate = true;
    float timeUntilNextWander = 0f;

    protected bool keepOrder = false;
    protected Vector2 stayDurationRange = new Vector2(5f, 15f);
    public IEnumerator Wander()
    {
        PointOfInterest target = null;
        bool hasArrived = true;
        bool nextTargetChosen = false;
        PointOfInterest nextTarget = null;
        while (true)
        {
            yield return new WaitUntil(() => !pauseWandering);

            // restore target after resume if it got changed
            if (target != null && agent.destination != target.transform.position)
                agent.SetDestination(target.transform.position);

            if (timeUntilNextWander <= 1f && !nextTargetChosen)
            {
                animator.Play("Idle");

                if (keepOrder)
                    nextTarget = pointsOfInterest[(pointsOfInterest.IndexOf(target) + 1) % (pointsOfInterest.Count)];
                else if (pointsOfInterest.Count > 1)
                {
                    var otherPOIs = pointsOfInterest.Where(POI => POI != target).ToArray();
                    int randomIndex = Random.Range(0, pointsOfInterest.Count - 1);
                    nextTarget = otherPOIs[randomIndex];
                }
                else
                    nextTarget = target != null ? target : pointsOfInterest[0];

                nextTargetChosen = true;

                RotateToward(nextTarget.transform.position);
            }

            if (timeUntilNextWander <= 0 && hasArrived)
            {
                animator.Play("Walking");

                timeUntilNextWander = Random.Range(stayDurationRange.x, stayDurationRange.y);

                target = nextTarget;
                nextTarget = null;
                nextTargetChosen = false;

                agent.SetDestination(target.transform.position);

                hasArrived = false;
            }
            else if (hasArrived)
            {
                timeUntilNextWander -= Time.deltaTime;
            }

            if (HasAgentArrived())
            {
                // reached destination

                if (shouldRotate && !nextTargetChosen)
                    RotateToward(target.transform.position + target.transform.forward);

                // play the animation, it needs to be in the controller and the state name needs to match the clip name, but like who ever changes that?
                // don't replay after it has finished
                if (!hasArrived)
                    animator.Play(target.animationClip.name);

                hasArrived = true;
            }

            if (!pauseWandering)
                animator.SetBool("IsWalking", agent.velocity.sqrMagnitude > 0.01f);
        }
    }

    protected bool HasAgentArrived()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    protected float rotationTime = 1f;
    public void RotateToward(Vector3 point)
    {
        transform.DOKill();
        transform.DOLookAt(point, rotationTime, AxisConstraint.Y, Vector3.up);
    }

    [Button] public void PauseWandering()
    {
        animator.SetBool("IsWalking", false);

        pauseWandering = true;
        agent.isStopped = true;
        shouldRotate = false;
    }

    [Button] public void ResumeWandering()
    {
        pauseWandering = false;
        agent.isStopped = false;
        shouldRotate = true;
    }
}