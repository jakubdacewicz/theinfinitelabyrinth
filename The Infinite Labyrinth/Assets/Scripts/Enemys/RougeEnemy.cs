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

            animator.SetBool("isRunning", false);

            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.SetBool("isRunning", true);
            animator.Play("Run");
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

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.SetBool("isRunning", true);
            animator.Play("Run");
        }

        if (IsEnemyAtPositionOfPoint())
        {
            agent.stoppingDistance = stats.attackRange.GetValue();
            agent.speed -= movementSpeedBoost;

            currentWaitTime = Time.time + waitTime;

            isRunningAway = false;
            isWaiting = true;

            animator.SetBool("isRunning", false);
        }
    }

    public override void Attack()
    {
        if (currentAttackWaitTime > Time.time)
        {
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        animator.Play("Attack");

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
