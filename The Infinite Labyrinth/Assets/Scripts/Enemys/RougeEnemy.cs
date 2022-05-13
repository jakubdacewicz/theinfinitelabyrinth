using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeEnemy : EnemyController
{
    public override void Follow()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= stats.attackRange.GetValue())
        {
            isFollowing = false;
            isAttacking = true;

            return;
        }

        agent.destination = player.transform.position;
    }

    public override void Wait()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (currentWaitTime <= Time.time)
        {
            currentDestination = FindPoint();

            isWaiting = false;
            isFollowing = true;
        }
    }

    public override void RunAway()
    {
        agent.stoppingDistance = 0;
        agent.destination = currentDestination.GetPointPosition;
        agent.speed = startMovementSpeed + movementSpeedBoost;

        if (IsEnemyAtPositionOfPoint())
        {
            agent.stoppingDistance = stats.attackRange.GetValue();
            agent.speed -= movementSpeedBoost;

            currentWaitTime = Time.time + waitTime;

            isRunningAway = false;
            isWaiting = true;
        }
    }

    public override void Attack()
    {
        if (currentAttackWaitTime > Time.time)
        {
            return;
        }

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

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

        isAttacking = false;
        isRunningAway = true;
    }
}
