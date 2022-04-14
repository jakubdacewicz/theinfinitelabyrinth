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

    public List<Transform> waitPoints;

    public float waitTime;
    public float movementSpeedBoost;
    public float startMovementSpeed;

    public float currentWaitTime;
    public float currentAttackWaitTime;

    private void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = stats.movementSpeed.GetValue();
        agent.stoppingDistance = stats.attackRange.GetValue();
        startMovementSpeed = stats.movementSpeed.GetValue();

        StartCoroutine(LoadPlayerData());
    }

    private IEnumerator LoadPlayerData()
    {
        yield return new WaitForSeconds(1);

        player = GameObject.FindWithTag("Player");
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

    public Vector3 FindClosestPoint()
    {
        Vector3 closestPoint = Vector3.zero;
        float distance = 9999;

        foreach (Transform point in waitPoints)
        {
            if (Vector3.Distance(point.position, gameObject.transform.position) < distance)
            {
                closestPoint = point.position;
                distance = Vector3.Distance(point.position, gameObject.transform.position);
            }
        }

        return closestPoint;
    }

    public abstract void Attack();

    public bool IsEnemyAtPositionOfPoint()
    {
        return Vector3.Distance(gameObject.transform.position, FindClosestPoint()) < 0.2f;
    }
}
