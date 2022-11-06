using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    public GameObject player;

    public EnemyStats stats;

    public NavMeshAgent agent;

    public bool isFollowing = false;
    public bool isAttacking = false;
    public bool isRunningAway = false;
    public bool isWaiting = false;

    public List<WaitPoint> waitPoints;

    public float waitTime;
    public float movementSpeedBoost;
    public float startMovementSpeed;

    public float currentWaitTime;
    public float currentAttackWaitTime;

    public WaitPoint currentDestination;
    private WaitPoint point;

    public Animator animator;

    private void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = stats.movementSpeed.GetValue();
        agent.stoppingDistance = stats.attackRange.GetValue();
        startMovementSpeed = stats.movementSpeed.GetValue();

        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine(LoadPlayerData());
    }

    private IEnumerator LoadPlayerData()
    {
        yield return new WaitForSeconds(1.2f);

        player = GameObject.FindWithTag("Player");

        currentWaitTime = Time.time + waitTime;
        isWaiting = true;
    }

    private void Update()
    {
        if (isAttacking)
        {
            Attack();
        }
        else if (isFollowing)
        {
            Follow();
        }
        else if (isWaiting)
        {
            Wait();
        }
        else if (isRunningAway)
        {
            RunAway();
        }
    }

    public abstract void Follow();
    public abstract void Wait();

    public virtual void RunAway()
    {
        isFollowing = true;
        isRunningAway = false;
    }

    public WaitPoint FindPoint()
    {
        float distance = 0;

        foreach (WaitPoint waitPoint in waitPoints)
        {
            if (Vector3.Distance(waitPoint.GetPointPosition, gameObject.transform.position) >= distance 
                && !waitPoint.IsPointUsed)
            {
                point = waitPoint;
                distance = Vector3.Distance(waitPoint.GetPointPosition, gameObject.transform.position);
            }
        }

        point.UsePoint(true);

        return point;
    }

    public abstract void Attack();

    public bool IsEnemyAtPositionOfPoint()
    {
        float dist = Vector3.Distance(transform.position, 
            new Vector3(currentDestination.GetPointPosition.x, transform.position.y, currentDestination.GetPointPosition.z));

        if(dist < 1)
        {
            currentDestination.UsePoint(false);
            return true;
        }
        return false;
    }
}
