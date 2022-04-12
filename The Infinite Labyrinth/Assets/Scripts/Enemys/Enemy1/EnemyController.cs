using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;

    private EnemyStats stats;

    //po testach zmienic na private
    public bool isFollowing = false;
    public bool isAttacking = false;
    public bool isRunningAway = false;
    public bool isWaiting = false;

    public List<Transform> waitPoints;

    public float waitTime;
    public float stopRange;

    private float currentWaitTime;
    private float currentAttackWaitTime;

    private void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();    
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
        else if(isRunningAway)
        {
            RunAway();
        }
    }

    private void Follow()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= stopRange)
        {
            isFollowing = false;
            isAttacking = true;

            return;
        }

        //transform.LookAt(player.transform.position);
        gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, stats.movementSpeed.GetValue() * Time.deltaTime);
    }

    private void Wait()
    {
       // transform.LookAt(player.transform.position);

        if (currentWaitTime <= Time.time)
        {
            isAttacking = true;
            isWaiting = false;
        }
    }

    private void RunAway()
    {
        //transform.LookAt(FindClosestPoint());
        gameObject.transform.position = Vector3.MoveTowards(transform.position, FindClosestPoint(), stats.movementSpeed.GetValue() * Time.deltaTime);

        if (IsEnemyAtPositionOfPoint())
        {
            isWaiting = true;
            isRunningAway = false;

            currentWaitTime = Time.time + waitTime;
        }
    }

    private Vector3 FindClosestPoint()
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

    private void Attack()
    {
        //lookat dodac
        if (stats.GetCurrentHealth() <= stats.maxHealth.GetValue() / 4)
        {           
            isFollowing = false;
            isWaiting = false;
            isRunningAway = true;
            isAttacking = false;
        }

        if (currentAttackWaitTime > Time.time)
        {
            return;
        }


        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, stats.attackRange.GetValue());

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player.GetComponent<CharacterStats>().TakeDamage(stats.attackDamage.GetValue());
                currentAttackWaitTime = Time.time + stats.attackCooldown.GetValue();

                break;
            }
        }

        isFollowing = true;
        isAttacking = false;
    }

    private bool IsEnemyAtPositionOfPoint()
    {
        return Vector3.Distance(gameObject.transform.position, FindClosestPoint()) < 0.2f;
    }
}
