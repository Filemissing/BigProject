using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    [Header("Dialogue")]
    [SerializeField] private DSP_CharacterAsset characterAsset;
    [SerializeField] private DSP_ConversationGraphAsset[] conversations;

    private int currentConversationIndex = 0;

    public void PlayDialogue()
    {
        PauseWandering();

        RotateToward(Quaternion.LookRotation(GameManager.instance.player.transform.position - transform.position).eulerAngles.y);

        DSP_ConversationManager.instance.StartConversation(conversations[currentConversationIndex]);

        // last conversation will be repeated
        if (currentConversationIndex < conversations.Length - 1)
        {
            currentConversationIndex++;
        }
    }

    [Header("Wandering")]
    [SerializeField] private PointOfInterest[] pointsOfInterest;

    private void Start()
    {
        StartCoroutine(Wander());
        DSP_ConversationManager.instance.OnConversationEnded += ResumeWandering;
    }

    bool pauseWandering = false;
    float timeUntilNextWander = 0f;
    public IEnumerator Wander()
    {
        PointOfInterest target = null;
        while (true)
        {
            if (timeUntilNextWander <= 0)
            {
                target = pointsOfInterest[Random.Range(0, pointsOfInterest.Length)];
                timeUntilNextWander = Random.Range(5f, 15f);
                agent.SetDestination(target.transform.position);
            }
            else
            {
                timeUntilNextWander -= Time.deltaTime;
            }

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // reached destination
                    RotateToward(target.transform.eulerAngles.y);
                }
            }

            yield return new WaitUntil(() => !pauseWandering);
        }
    }

    public void RotateToward(float yRotation)
    {
        transform.DOKill();
        Quaternion targetRotation = Quaternion.Euler(0, yRotation, 0);
        transform.DORotate(targetRotation.eulerAngles, 1f);
    }

    [Button] public void PauseWandering()
    {
        pauseWandering = true;
        agent.isStopped = true;
    }

    [Button] public void ResumeWandering()
    {
        pauseWandering = false;
        agent.isStopped = false;
    }
}